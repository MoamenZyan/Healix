namespace Healix.Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public AppointmentStatus Status { get; set; }
        public DateOnly Day { get; set; }
        public TimeOnly From { get; set; }
        public TimeOnly To { get; set; }

        public virtual ApplicationUser Doctor { get; set; } = null!;
        public virtual ApplicationUser Patient { get; set; } = null!;

        public AppointmentMinimalDto ToMinimalDto()
        {
            var dto = new AppointmentMinimalDto()
            {
                Id = this.Id,
                IsDeleted = this.IsDeleted,
                CreatedAt = this.CreatedAt,
                Day = this.Day,
                From = this.From,
                Status = this.Status,
                To = this.To,
            };

            return dto;
        }

        public AppointmentDto ToDto()
        {
            var dto = new AppointmentDto()
            {
                Id = this.Id,
                IsDeleted = this.IsDeleted,
                CreatedAt = this.CreatedAt,
                Day = this.Day,
                From = this.From,
                To = this.To,
                Status = this.Status,
                Patient = this.Patient?.ToLoginUserDto(),
            };

            return dto;
        }
    }

    public class AppointmentMinimalDto : BaseEntity
    {
        public AppointmentStatus Status { get; set; }
        public DateOnly Day { get; set; }
        public TimeOnly From { get; set; }
        public TimeOnly To { get; set; }
    }

    public class AppointmentDto : BaseEntity
    {
        public AppointmentStatus Status { get; set; }
        public DateOnly Day { get; set; }
        public TimeOnly From { get; set; }
        public TimeOnly To { get; set; }

        public LoginUserDto? Patient { get; set; } = null;
    }

    public class AppointmentsByDayDto
    {
        public DateOnly Day { get; set; }
        public List<AppointmentDto> Appointments { get; set; } = null!;
    }
}
