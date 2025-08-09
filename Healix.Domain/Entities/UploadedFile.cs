using Healix.Domain.Entities;

public class UploadedFile : BaseEntity
{
    public string FileUrl { get; set; } = null!;
    public Guid PatientRecordId { get; set; }

    public virtual PatientRecord PatientRecord { get; set; } = null!;

    public UploadedFileDto ToDto()
    {
        var dto = new UploadedFileDto()
        {
            Id = this.Id,
            CreatedAt = this.CreatedAt,
            FileUrl = this.FileUrl,
        };

        return dto;
    }
}

public class UploadedFileDto : BaseEntity
{
    public string FileUrl { get; set; } = null!;
}
