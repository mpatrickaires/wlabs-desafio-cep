namespace WLabsDesafioCEP.Domain.Exceptions
{
    public class CepInvalidoException : Exception
    {
        public CepInvalidoException(string mensagem) 
            : base(mensagem)
        { 
        }
    }
}
