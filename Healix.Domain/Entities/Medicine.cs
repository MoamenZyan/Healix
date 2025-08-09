using Healix.Domain.Entities;

public class Medicine : BaseEntity
{
    public string? MedicineName { get; set; } = null!;
    public string? Frequency { get; set; } = null!;
    public DateTime? EndDate { get; set; } = null!;
    public DateTime? StartDate { get; set; }

    public Guid PatientRecordId { get; set; }
    public virtual PatientRecord PatientRecord { get; set; } = null!;

    public MedicineDto ToDto()
    {
        var dto = new MedicineDto()
        {
            MedicineName = this.MedicineName,
            Frequency = this.Frequency,
            EndDate = this.EndDate,
            StartDate = this.StartDate,
        };

        return dto;
    }
}

public class MedicineDto : BaseEntity
{
    public string? MedicineName { get; set; } = null!;
    public string? Frequency { get; set; } = null!;
    public DateTime? EndDate { get; set; } = null!;
    public DateTime? StartDate { get; set; }
}
