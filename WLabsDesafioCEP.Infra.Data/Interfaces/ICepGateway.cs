using WLabsDesafioCEP.Domain.Entities;
using WLabsDesafioCEP.Domain.ValueObjects;

namespace WLabsDesafioCEP.Infra.Data.Interfaces
{
    public interface ICepGateway
    {
        Task<Endereco> ObterEnderecoPeloCepAsync(Cep cep);
    }
}
