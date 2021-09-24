using System;

namespace AccountService.Exceptions
{
    public class CustomException : Exception
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public CustomException()
        {
            StatusCode = 500;
            ErrorMessage = "Internal server error.";
        }
    }
}
