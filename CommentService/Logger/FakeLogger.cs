using Microsoft.Extensions.Logging;

namespace CommentService.Logger
{
    public class FakeLogger
    {
        private readonly ILogger<FakeLogger> _logger;

        public FakeLogger(ILogger<FakeLogger> logger)
        {
            _logger = logger;
        }

        public void Log(string message)
        {
            _logger.LogInformation(message);
        }
    }
}
