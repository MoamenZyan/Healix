using Healix.Domain.Entities;

public class Otp : BaseEntity
{
    public string Secret { get; set; } = OtpSecret.GetRandomOTP();
    public DateTime Expiration { get; set; } = DateTime.UtcNow.AddMinutes(6);
    public OtpStatus Status { get; set; } = OtpStatus.Pending;
}

public static class OtpSecret
{
    public static string GetRandomOTP()
    {
        Random random = new Random();
        int otp = random.Next(100000, 1000000);
        return otp.ToString();
    }
}
