using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

public class WelcomeEmailStrategy : IEmailStrategy
{
    private readonly SendGridConfiguration _configuration;

    public WelcomeEmailStrategy(IOptions<SendGridConfiguration> configuration)
    {
        _configuration = configuration.Value;
    }

    public async Task<bool> Send(dynamic content)
    {
        var apiKey = _configuration.ApiKey;
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress(_configuration.FromEmail, "Healix");
        var subject = "Welcome To Healix!";

        var to = new EmailAddress(
            Convert.ToString(content["Email"]),
            Convert.ToString(content["Username"])
        );

        var htmlTemplate = await File.ReadAllTextAsync(
            "../Healix.Infrastructure/Services/Email/Templates/WelcomeEmailTemplate.html"
        );

        var htmlContent = htmlTemplate
            .Replace("{{PhotoURL}}", Convert.ToString(content["PhotoURL"]))
            .Replace("{{Username}}", Convert.ToString(content["Username"]));

        var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);
        var response = await client.SendEmailAsync(msg);

        return response.IsSuccessStatusCode;
    }
}
