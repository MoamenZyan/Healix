using Microsoft.Extensions.DependencyInjection;

namespace Healix.Infrastructure.Services;

public class EmailContext : IEmailContext
{
    private readonly IServiceProvider _serviceProvider;

    public EmailContext(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IEmailStrategy? GetEmailStrategy(EmailType type)
    {
        return type switch
        {
            EmailType.Welcome => _serviceProvider.GetRequiredService<WelcomeEmailStrategy>(),
            EmailType.OTP => _serviceProvider.GetRequiredService<OTPEmailStrategy>(),
            _ => null,
        };
    }
}
