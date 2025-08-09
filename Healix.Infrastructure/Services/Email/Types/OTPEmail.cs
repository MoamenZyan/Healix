using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

public class OTPEmailStrategy : IEmailStrategy
{
    private readonly SendGridConfiguration _configuration;

    public OTPEmailStrategy(IOptions<SendGridConfiguration> configuration)
    {
        _configuration = configuration.Value;
    }

    public async Task<bool> Send(dynamic content)
    {
        var apiKey = _configuration.ApiKey;
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress(_configuration.FromEmail, "Healix");
        var subject = "Your OTP!";

        var to = new EmailAddress(Convert.ToString(content["Email"]));

        var htmlTemplate = await File.ReadAllTextAsync(
            "../Healix.Infrastructure/Services/Email/Templates/OTPEmailTemplate.html"
        );

        var htmlContent = htmlTemplate.Replace("{{OTP}}", Convert.ToString(content["OTP"]));

        var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);
        SendGrid.Response response = await client.SendEmailAsync(msg);

        return response.IsSuccessStatusCode;
    }
}
