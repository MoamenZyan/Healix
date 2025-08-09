public interface IEmailStrategy
{
    Task<bool> Send(dynamic content);
}
