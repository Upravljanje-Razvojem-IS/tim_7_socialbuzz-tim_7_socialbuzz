namespace AccountService.Exceptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException()
            : base()
        {
            StatusCode = 404;
            ErrorMessage = "The server can not find the requested resource..";
        }

        public NotFoundException(string message)
            : this()
        {
            ErrorMessage = message;
        }
    }
}
