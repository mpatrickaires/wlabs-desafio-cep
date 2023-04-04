using Microsoft.Extensions.Logging;
using WLabsDesafioCEP.Common.Interfaces;
using WLabsDesafioCEP.Common.Logging;

namespace WLabsDesafioCEP.Infra.Data.Logging
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger<LoggerService> _logger;

        public LoggerService(ILogger<LoggerService> logger)
        {
            _logger = logger;
        }

        public void LogInformation(string mensagem)
        {
            _logger.LogInformation("{@Template}", new LogTemplate("Information", mensagem));
        }

        public void LogWarning(string mensagem)
        {
            _logger.LogWarning("{@Template}", new LogTemplate("Warning", mensagem));
        }

        public void LogError(string mensagem, string? mensagemException = null, string? stackTrace = null)
        {
            _logger.LogError("{@Template}", new LogTemplate("Error", mensagem, mensagemException, stackTrace));
        }


    }
}
