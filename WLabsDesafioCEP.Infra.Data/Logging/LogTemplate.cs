namespace WLabsDesafioCEP.Common.Logging
{
    public class LogTemplate
    {

        public string Level { get; set; }
        public string Mensagem { get; set; }
        public string? MensagemException { get; set; }
        public string? StackTrace { get; set; }
        public DateTime Timestamp { get; set; }

        public LogTemplate(string level, string mensagem, string? mensagemException = null, string? stackTrace = null, 
            DateTime? timestamp = null)
        {
            Level = level;
            Mensagem = mensagem;
            MensagemException = mensagemException;
            StackTrace = stackTrace;
            Timestamp = timestamp ?? DateTime.UtcNow;
        }
    }
}
