namespace Healix.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public int ErrorCode { get; }

        public NotFoundException(string message)
            : base(message)
        {
            ErrorCode = 404;
        }
    }
}
