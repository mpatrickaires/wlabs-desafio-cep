using Refit;
using System.Net;
using WLabsDesafioCEP.Domain.Exceptions;
using WLabsDesafioCEP.Domain.ValueObjects;
using WLabsDesafioCEP.Infra.Data.Common.Dtos;
using WLabsDesafioCEP.Infra.Data.Interfaces;
using WLabsDesafioCEP.Infra.Data.Refit;

namespace WLabsDesafioCEP.Infra.Data.Clients
{
    public class ApiCepClient : IApiCepClient
    {
        private readonly IApiCepRefit _apiCepRefitClient;

        public ApiCepClient(IApiCepRefit apiCepRefitClient)
        {
            _apiCepRefitClient = apiCepRefitClient;
        }

        public async Task<EnderecoApiCepDto> ObterEnderecoPeloCepAsync(Cep cep)
        {
            try
            {
                EnderecoApiCepDto enderecoDto = await _apiCepRefitClient.ObterEnderecoPeloCepAsync(cep.ValorComSeparador);
                return enderecoDto;
            }
            catch (ApiException e) when (e.StatusCode == HttpStatusCode.NotFound)
            {
                throw new CepInexistenteException();
            }
        }
    }
}
