using Refit;
using WLabsDesafioCEP.Infra.Data.Common.Dtos;

namespace WLabsDesafioCEP.Infra.Data.Refit
{
    public interface IViaCepRefit
    {
        [Get("/{cep}/json")]
        Task<EnderecoViaCepDto> ObterEnderecoPeloCepAsync(string cep);
    }
}
