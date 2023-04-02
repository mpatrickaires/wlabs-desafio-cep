namespace WLabsDesafioCEP.WebAPI.Common.Dtos
{
    public class RespostaApiDto<T>
    {
        public T Dados { get; set; }
        public string? Mensagem { get; set; }

        public RespostaApiDto(T dados, string? mensagem = null)
        {
            Dados = dados;
            Mensagem = mensagem;
        }

    }

    public class RespostaApiDto
    {
        public string? Mensagem { get; set; }

        public RespostaApiDto(string? mensagem = null)
        {
            Mensagem = mensagem;
        }
    }
}
