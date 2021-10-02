namespace AccountService.Exceptions
{
    public class ConflictException : CustomException
    {
        public ConflictException()
        : base()
        {
            StatusCode = 409;
            ErrorMessage = "Conflict encountered.";
        }

        public ConflictException(string message)
            : this()
        {
            ErrorMessage = message;
        }
    }
}
