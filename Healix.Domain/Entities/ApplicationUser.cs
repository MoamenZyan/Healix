using Healix.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Healix.Domain.Entities
{
    public class ApplicationUser : BaseEntity
    {
        public string Fname { get; set; } = null!;
        public string Lname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int? YearsOfExperience { get; set; }
        public string? ProfessionalTitle { get; set; } = null!;
        public DoctorSpeciality DoctorSpeciality { get; set; }
        public string? PracticeLicenseUrl { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public UserType UserType { get; set; } = UserType.Patient;
        public string? PhotoUrl { get; set; } = null!;
        public Location Location { get; set; } = null!;
        public BloodType BloodType { get; set; }
        public string Height { get; set; } = null!;
        public string Weight { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string NationalId { get; set; } = null!;
        public Guid? FamilyGroupId { get; set; }
        public Guid? DoctorScheduleId { get; set; }

        public virtual Guid PatientSummaryId { get; set; }
        public virtual PatientSummary PatientSummary { get; set; } = null!;
        public virtual Clinic Clinic { get; set; } = null!;
        public virtual List<UserChronicDisease> ChronicDiseases { get; set; } =
            new List<UserChronicDisease>();
        public virtual List<PatientRecord> PatientRecords { get; set; } = new List<PatientRecord>();
        public virtual List<Appointment> DoctorAppointments { get; set; } = new List<Appointment>();
        public virtual List<Appointment> PatientAppointments { get; set; } =
            new List<Appointment>();
        public virtual List<ChatBot> ChatBots { get; set; } = new List<ChatBot>();
        public virtual FamilyGroup FamilyGroup { get; set; } = null!;
        public virtual DoctorSchedule DoctorSchedule { get; set; } = null!;
        public virtual List<DoctorCertificate> DoctorCertificates { get; set; } =
            new List<DoctorCertificate>();

        public DoctorUserDto ToDoctorDto()
        {
            var user = new DoctorUserDto()
            {
                Id = this.Id,
                Fname = this.Fname,
                Lname = this.Lname,
                Email = this.Email,
                PhotoUrl = this.PhotoUrl,
                PhoneNumber = this.PhoneNumber,
                YearsOfExperience = this.YearsOfExperience,
                ProfessionalTitle = this.ProfessionalTitle,
                UserType = this.UserType.ToString(),
                DoctorSpeciality = this.UserType == UserType.Doctor ? this.DoctorSpeciality : null,
                CreatedAt = this.CreatedAt,
                Location = this.Location,
                BloodType = this.BloodType,
                Height = this.Height,
                Weight = this.Weight,
                DateOfBirth = this.DateOfBirth,
                Gender = this.Gender,
                NationalId = this.NationalId,
                Clinic = this.Clinic?.ToDto(),
                PendingAppointmentsCount = this
                    .DoctorAppointments?.Where(x => x.Status == AppointmentStatus.Pending)
                    .Count(),
                CancelledAppointmentsCount = this
                    .DoctorAppointments?.Where(x => x.Status == AppointmentStatus.Cancelled)
                    .Count(),
                CheckedInAppointmentsCount = this
                    .DoctorAppointments?.Where(x => x.Status == AppointmentStatus.CheckedIn)
                    .Count(),
                CompletedAppointmentsCount = this
                    .DoctorAppointments?.Where(x => x.Status == AppointmentStatus.Completed)
                    .Count(),
                UpcomingAppointments = this
                    .DoctorAppointments?.Where(x => x.Day > DateOnly.FromDateTime(DateTime.Today))
                    .Select(x => x.ToDto())
                    .ToList(),
                PreviousAppointments = this
                    .DoctorAppointments?.Where(x => x.Day < DateOnly.FromDateTime(DateTime.Today))
                    .Select(x => x.ToDto())
                    .ToList(),
                DoctorCertificates = this.DoctorCertificates?.Select(x => x.ToDto()).ToList(),
            };

            return user;
        }

        public PatientUserDto ToPatientDto()
        {
            var user = new PatientUserDto()
            {
                Id = this.Id,
                Fname = this.Fname,
                Lname = this.Lname,
                Email = this.Email,
                PhotoUrl = this.PhotoUrl,
                PhoneNumber = this.PhoneNumber,
                UserType = this.UserType.ToString(),
                CreatedAt = this.CreatedAt,
                Location = this.Location,
                BloodType = this.BloodType,
                Height = this.Height,
                Weight = this.Weight,
                DateOfBirth = this.DateOfBirth,
                Gender = this.Gender,
                NationalId = this.NationalId,
                ChronicDiseases = this.ChronicDiseases?.Select(x => x.ToDto()).ToList(),
                UpcomingAppointments = this
                    .PatientAppointments?.Where(x => x.Day > DateOnly.FromDateTime(DateTime.Today))
                    .Select(x => x.ToDto())
                    .ToList(),
                PreviousAppointments = this
                    .PatientAppointments?.Where(x => x.Day < DateOnly.FromDateTime(DateTime.Today))
                    .Select(x => x.ToDto())
                    .ToList(),
            };

            return user;
        }

        public LoginUserDto ToLoginUserDto()
        {
            var user = new LoginUserDto()
            {
                Id = this.Id,
                Fname = this.Fname,
                Lname = this.Lname,
                Email = this.Email,
                PhotoUrl = this.PhotoUrl,
                PhoneNumber = this.PhoneNumber,
                UserType = this.UserType.ToString(),
                CreatedAt = this.CreatedAt,
                DateOfBirth = this.DateOfBirth,
                Gender = this.Gender,
                NationalId = this.NationalId,
            };

            return user;
        }
    }

    public class DoctorUserDto : BaseEntity
    {
        public string Fname { get; set; } = null!;
        public string Lname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhotoUrl { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int? YearsOfExperience { get; set; }
        public string? ProfessionalTitle { get; set; } = null!;
        public DoctorSpeciality? DoctorSpeciality { get; set; }
        public string UserType { get; set; } = null!;
        public Location? Location { get; set; } = null!;
        public BloodType BloodType { get; set; }
        public string Height { get; set; } = null!;
        public string Weight { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string NationalId { get; set; } = null!;
        public int? CheckedInAppointmentsCount { get; set; } = null!;
        public int? CompletedAppointmentsCount { get; set; } = null!;
        public int? CancelledAppointmentsCount { get; set; } = null!;
        public int? PendingAppointmentsCount { get; set; } = null!;
        public virtual List<AppointmentDto>? UpcomingAppointments { get; set; } = null!;
        public virtual List<AppointmentDto>? PreviousAppointments { get; set; } = null!;
        public virtual List<DoctorCertificateDto>? DoctorCertificates { get; set; } = null!;
        public virtual ClinicDto? Clinic { get; set; } = null!;
    }

    public class ApplicationUserWrapper
    {
        public string UserType { get; private set; } = null!;
        public object User { get; private set; } = null!;

        public ApplicationUserWrapper(UserType? type, ApplicationUser user)
        {
            UserType = type.ToString();

            User = type switch
            {
                Enums.UserType.Patient => user.ToPatientDto(),
                Enums.UserType.Doctor => user.ToDoctorDto(),
                _ => user.ToLoginUserDto(),
            };
        }
    }

    public class LoginUserDto : BaseEntity
    {
        public string Fname { get; set; } = null!;
        public string Lname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhotoUrl { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string UserType { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string NationalId { get; set; } = null!;
    }

    public class PatientUserDto : BaseEntity
    {
        public string Fname { get; set; } = null!;
        public string Lname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhotoUrl { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string UserType { get; set; } = null!;
        public Location? Location { get; set; } = null!;
        public BloodType BloodType { get; set; }
        public string Height { get; set; } = null!;
        public string Weight { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string NationalId { get; set; } = null!;
        public virtual List<UserChronicDiseaseDto>? ChronicDiseases { get; set; } = null!;
        public virtual List<AppointmentDto>? UpcomingAppointments { get; set; } = null!;
        public virtual List<AppointmentDto>? PreviousAppointments { get; set; } = null!;
    }

    public class UpdateUserDto
    {
        public Guid Id { get; set; }
        public string? Fname { get; set; } = null!;
        public string? Lname { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public IFormFile? Photo { get; set; } = null!;
        public string? PhoneNumber { get; set; } = null!;
        public DoctorSpeciality? DoctorSpeciality { get; set; }
        public string? UserType { get; set; } = null!;
        public Location? Location { get; set; } = null!;
        public BloodType? BloodType { get; set; }
        public string? Height { get; set; } = null!;
        public string? Weight { get; set; } = null!;
        public DateOnly? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public string? NationalId { get; set; } = null!;

        public ApplicationUser ToEntity(ApplicationUser user, string? photoUrl)
        {
            if (Fname != null)
                user.Fname = Fname;

            if (Lname != null)
                user.Lname = Lname;

            if (Email != null)
                user.Email = Email;

            if (photoUrl != null)
                user.PhotoUrl = photoUrl;

            if (DoctorSpeciality != null)
                user.DoctorSpeciality = (DoctorSpeciality)DoctorSpeciality!;

            if (UserType != null)
                user.UserType = (UserType)Enum.Parse(typeof(UserType), UserType);

            if (Location != null)
                user.Location = Location;

            if (BloodType != null)
                user.BloodType = (BloodType)BloodType;

            if (Height != null)
                user.Height = Height;

            if (Weight != null)
                user.Weight = Weight;

            if (DateOfBirth != null)
                user.DateOfBirth = (DateOnly)DateOfBirth;

            if (Gender != null)
                user.Gender = (Gender)Gender;

            if (NationalId != null)
                user.NationalId = NationalId;

            return user;
        }
    }
}
