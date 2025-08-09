namespace Healix.Application.Exceptions
{
    public class ConflictException : Exception
    {
        public int ErrorCode { get; }

        public ConflictException(string message)
            : base(message)
        {
            ErrorCode = 409;
        }
    }
}
