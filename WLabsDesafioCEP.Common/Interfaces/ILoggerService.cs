namespace WLabsDesafioCEP.Common.Interfaces
{
    public interface ILoggerService
    {
        void LogInformation(string mensagem);
        void LogWarning(string mensagem);
        void LogError(string mensagem, string? mensagemException = null, string? stackTrace = null);
    }
}
