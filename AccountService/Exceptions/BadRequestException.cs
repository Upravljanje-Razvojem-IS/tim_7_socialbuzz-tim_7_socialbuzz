namespace AccountService.Exceptions
{
    public class BadRequestException : CustomException
    {
        public BadRequestException()
        : base()
        {
            StatusCode = 400;
            ErrorMessage = "Bad request.";
        }

        public BadRequestException(string message)
            : this()
        {
            ErrorMessage = message;
        }
    }
}
