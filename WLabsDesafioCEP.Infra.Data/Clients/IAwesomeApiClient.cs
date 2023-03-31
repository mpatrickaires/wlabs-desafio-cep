using Refit;
using WLabsDesafioCEP.Infra.Data.Common.Dtos;

namespace WLabsDesafioCEP.Infra.Data.Clients
{
    public interface IAwesomeApiClient
    {
        [Get("/{cep}")]
        Task<EnderecoAwesomeApiDto> ObterEnderecoPeloCep(string cep);
    }
}
