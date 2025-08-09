// using Healix.Domain.Entities;
// using MediatR;
// using Microsoft.EntityFrameworkCore;

// public class EditRecordHandler : IRequestHandler<EditRecordCommand, PatientRecordDto>
// {
//     private readonly IApplicationDbContext _context;
//     private readonly IS3Service _s3Service;

//     public EditRecordHandler(IApplicationDbContext context, IS3Service s3Service)
//     {
//         _context = context;
//         _s3Service = s3Service;
//     }

//     public async Task<PatientRecordDto> Handle(
//         EditRecordCommand request,
//         CancellationToken cancellationToken
//     )
//     {
//         var record = await _context.PatientRecords.FirstOrDefaultAsync(x =>
//             x.Id == request.RecordId
//         );

//         if (request.Notes != null)
//             record!.Notes = request.Notes;

//         if (request.File != null)
//         {
//             var url = await _s3Service.UploadFile(request.File);
//             record!.FileUrl = url;
//         }

//         await _context.PatientRecords.AddAsync(record!, cancellationToken);
//         await _context.SaveChangesAsync(cancellationToken);

//         return record!.ToDto(null);
//     }
// }
