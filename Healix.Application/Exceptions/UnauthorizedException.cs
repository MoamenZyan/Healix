namespace Healix.Application.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public int ErrorCode { get; }

        public UnauthorizedException(string message)
            : base(message)
        {
            ErrorCode = 401;
        }
    }
}
