using Refit;
using WLabsDesafioCEP.Infra.Data.Common.Dtos;

namespace WLabsDesafioCEP.Infra.Data.Refit
{
    public interface IAwesomeApiRefit
    {
        [Get("/{cep}")]
        Task<EnderecoAwesomeApiDto> ObterEnderecoPeloCepAsync(string cep);
    }
}
