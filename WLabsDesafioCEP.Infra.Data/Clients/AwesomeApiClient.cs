using Refit;
using System.Net;
using WLabsDesafioCEP.Domain.Exceptions;
using WLabsDesafioCEP.Domain.ValueObjects;
using WLabsDesafioCEP.Infra.Data.Common.Dtos;
using WLabsDesafioCEP.Infra.Data.Interfaces;
using WLabsDesafioCEP.Infra.Data.Refit;

namespace WLabsDesafioCEP.Infra.Data.Clients
{
    public class AwesomeApiClient : IAwesomeApiClient
    {
        private readonly IAwesomeApiRefit _awesomeApiRefitClient;

        public AwesomeApiClient(IAwesomeApiRefit awesomeApiRefitClient)
        {
            _awesomeApiRefitClient = awesomeApiRefitClient;
        }

        public async Task<IMapeavelParaEndereco> ObterEnderecoPeloCepAsync(Cep cep)
        {
            try
            {
                EnderecoAwesomeApiDto enderecoDto = await _awesomeApiRefitClient.ObterEnderecoPeloCepAsync(cep.Valor);
                return enderecoDto;
            }
            catch (ApiException e) when (e.StatusCode == HttpStatusCode.NotFound)
            {
                throw new CepInexistenteException();
            }
        }
    }
}
