using Refit;
using WLabsDesafioCEP.Infra.Data.Common.Dtos;

namespace WLabsDesafioCEP.Infra.Data.Clients
{
    public interface IApiCepClient
    {
        [Get("/{cep}.json")]
        Task<EnderecoApiCepDto> ObterEnderecoPeloCep(string cep);
    }
}
