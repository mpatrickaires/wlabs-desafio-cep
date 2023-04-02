using Refit;
using WLabsDesafioCEP.Infra.Data.Common.Dtos;

namespace WLabsDesafioCEP.Infra.Data.Refit
{
    public interface IApiCepRefit
    {
        [Get("/{cep}.json")]
        Task<EnderecoApiCepDto> ObterEnderecoPeloCepAsync(string cep);
    }
}
