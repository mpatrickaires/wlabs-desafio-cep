using Refit;
using WLabsDesafioCEP.Infra.Data.Common.Dtos;

namespace WLabsDesafioCEP.Infra.Data.Clients
{
    public interface IViaCepClient
    {
        [Get("/{cep}/json")]
        Task<EnderecoViaCepDto> ObterEnderecoPeloCep(string cep);
    }
}
