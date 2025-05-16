namespace CoreContato.Logs
{
    using Microsoft.Extensions.Logging;

    namespace APITesteConhecimento.Logging
    {
        public class Log<T>
        {
            private readonly ILogger<T> _logger;

            public Log(ILogger<T> logger)
            {
                _logger = logger;
            }

            public void Info(string message, params object[] args)
            {
                _logger.LogInformation(message, args);
            }


            public void Error(string message, Exception ex = null, params object[] args)
            {
                if (ex != null)
                {
                    _logger.LogError(ex, message, args);
                }
                else
                {
                    _logger.LogError(message, args);
                }
            }

        }
    }

}
