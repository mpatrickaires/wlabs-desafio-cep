namespace WLabsDesafioCEP.WebAPI.Common.Dtos
{
    public class RespostaApiDto<T>
    {
        public T? Dados { get; set; }
        public string? Mensagem { get; set; }
    }

    public class RespostaApiDto
    {
        public string? Mensagem { get; set; }
    }
}
