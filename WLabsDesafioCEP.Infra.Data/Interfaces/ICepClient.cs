using WLabsDesafioCEP.Domain.ValueObjects;

namespace WLabsDesafioCEP.Infra.Data.Interfaces
{
    public interface ICepClient
    {
        Task<IMapeavelParaEndereco> ObterEnderecoPeloCepAsync(Cep cep);
    }
}
