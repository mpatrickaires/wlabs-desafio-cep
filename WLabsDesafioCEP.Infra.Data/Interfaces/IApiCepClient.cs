using WLabsDesafioCEP.Domain.ValueObjects;
using WLabsDesafioCEP.Infra.Data.Common.Dtos;

namespace WLabsDesafioCEP.Infra.Data.Interfaces
{
    public interface IApiCepClient
    {
        Task<EnderecoApiCepDto> ObterEnderecoPeloCepAsync(Cep cep);
    }
}
