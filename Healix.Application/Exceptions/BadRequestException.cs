namespace Healix.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public int ErrorCode { get; }

        public BadRequestException(string message)
            : base(message)
        {
            ErrorCode = 400;
        }
    }
}
