namespace WLabsDesafioCEP.Infra.Data.Interfaces
{
    public interface ICacheGateway
    {
        Task<string?> ObterAsync(string chave);
        Task<T?> ObterDesserializadoAsync<T>(string chave, bool removerSeDesserializacaoFalhar = true)
            where T : class;
        Task SalvarAsync<T>(string chave, T valor);
        Task RemoverAsync(string chave);
    }
}
