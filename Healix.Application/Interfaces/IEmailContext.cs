public interface IEmailContext
{
    public IEmailStrategy? GetEmailStrategy(EmailType type);
}
