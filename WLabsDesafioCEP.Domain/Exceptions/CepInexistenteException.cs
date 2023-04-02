namespace WLabsDesafioCEP.Domain.Exceptions
{
    public class CepInexistenteException : Exception
    {
        private const string MensagemPadrao = "CEP inexistente!";

        public CepInexistenteException(string mensagem = MensagemPadrao) 
            : base(mensagem)
        {
        }
    }
}
