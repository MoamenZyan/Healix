using System.Security.Claims;
using Hangfire;
using Healix.Application.Exceptions;
using Healix.Domain.Entities;
using Healix.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Healix.Application.Modules;

public class UserSignupHandler
    : IRequestHandler<UserSignupCommand, (ApplicationUserWrapper, string)>
{
    private readonly IApplicationDbContext _context;
    private readonly IBackgroundJobClient _backgroundClient;
    private readonly IEmailContext _emailContext;
    private readonly IS3Service _s3Service;
    private readonly IMediator _mediator;
    private readonly IJwtToken _jwtToken;

    public UserSignupHandler(
        IApplicationDbContext context,
        IBackgroundJobClient backgroundClient,
        IEmailContext emailContext,
        IS3Service s3Service,
        IMediator mediator,
        IJwtToken jwtToken
    )
    {
        _context = context;
        _backgroundClient = backgroundClient;
        _emailContext = emailContext;
        _s3Service = s3Service;
        _mediator = mediator;
        _jwtToken = jwtToken;
    }

    public async Task<(ApplicationUserWrapper, string)> Handle(
        UserSignupCommand request,
        CancellationToken cancellationToken
    )
    {
        var user = await _context.Users.FirstOrDefaultAsync(
            x => x.Email == request.Email,
            cancellationToken
        );

        if (user != null)
            throw new ConflictException("email already exists");

        user = await _context.Users.FirstOrDefaultAsync(
            x => x.PhoneNumber == request.PhoneNumber,
            cancellationToken
        );

        if (user != null)
            throw new ConflictException("phone number already exists");

        if (request.Password != request.ConfirmPassword)
            throw new BadRequestException("Confirm password not equal password!");

        string? photoUrl = null;

        if (request.ProfilePhoto != null)
            photoUrl = await _s3Service.UploadFile(request.ProfilePhoto!);

        var names = request.FullName!.Split(" ").ToList();
        var fName = names[0];
        var lName = "";

        if (names.Count() >= 2)
            lName = names[1];

        user = new ApplicationUser()
        {
            Id = Guid.NewGuid(),
            Fname = fName,
            Lname = lName,
            PhoneNumber = request.PhoneNumber!,
            Email = request.Email!,
            PhotoUrl = photoUrl,
            UserType = (UserType)Enum.Parse(typeof(UserType), request.UserType!),
            Address = "N/A",
            Location = request.Location!,
            YearsOfExperience = request.YearsOfExperience,
            ProfessionalTitle = request.ProfessionalTitle,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password, 10),
            Height = request.Height!,
            Weight = request.Weight!,
            DateOfBirth = (DateOnly)request.DateOfBirth!,
            BloodType = (BloodType)request.BloodType!,
            Gender = request.Gender,
            NationalId = request.NationalId!,
        };

        if (request.DoctorSpeciality != null)
            user.DoctorSpeciality = (DoctorSpeciality)request.DoctorSpeciality;

        if (request.LicensePractice != null)
        {
            var fileUrl = await _s3Service.UploadFile(request.LicensePractice);
            user.PracticeLicenseUrl = fileUrl;
        }

        await _context.Users.AddAsync(user, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        var emailSender = _emailContext.GetEmailStrategy(EmailType.Welcome);

        var obj = new Dictionary<string, string>
        {
            { "Email", user.Email },
            { "Username", user.Fname },
            { "PhotoURL", photoUrl! },
        };

        _backgroundClient.Enqueue(() => emailSender!.Send(obj));

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber!),
            new Claim(ClaimTypes.Role, user.UserType.ToString()),
        };

        var token = _jwtToken.GenerateJwtToken(claims);

        var wrapper = new ApplicationUserWrapper(null, user);

        return (wrapper, token);
    }
}
