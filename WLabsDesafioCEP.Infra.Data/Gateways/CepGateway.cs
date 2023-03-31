using WLabsDesafioCEP.Domain.Entities;
using WLabsDesafioCEP.Domain.ValueObjects;
using WLabsDesafioCEP.Infra.Data.Clients;
using WLabsDesafioCEP.Infra.Data.Common.Dtos;
using WLabsDesafioCEP.Infra.Data.Common.Mappings;
using WLabsDesafioCEP.Infra.Data.Interfaces;

namespace WLabsDesafioCEP.Infra.Data.Gateways
{
    public class CepGateway : ICepGateway
    {
        private readonly IApiCepClient _apiCepClient;
        private readonly IAwesomeApiClient _awesomeApiClient;
        private readonly IViaCepClient _viaCepClient;

        public CepGateway(IApiCepClient apiCepClient, IAwesomeApiClient awesomeApiClient, IViaCepClient viaCepClient)
        {
            _apiCepClient = apiCepClient;
            _awesomeApiClient = awesomeApiClient;
            _viaCepClient = viaCepClient;
        }

        public async Task<Endereco> ObterEnderecoPeloCepAsync(Cep cep)
        {
            Task<EnderecoViaCepDto> viaCepTask = _viaCepClient.ObterEnderecoPeloCep(cep.Valor);
            Task<EnderecoApiCepDto> apiCepTask = _apiCepClient.ObterEnderecoPeloCep(cep.ValorComSeparador);
            Task<EnderecoAwesomeApiDto> awesomeApiTask = _awesomeApiClient.ObterEnderecoPeloCep(cep.Valor);

            var tasks = new List<Task> { viaCepTask, awesomeApiTask, apiCepTask };

            while (tasks.Any())
            {
                Task result = await Task.WhenAny(tasks);

                if (result == viaCepTask)
                {
                    EnderecoViaCepDto enderecoDto = await viaCepTask;
                    return enderecoDto.MapearParaEndereco();
                }

                if (result == apiCepTask)
                {
                    EnderecoApiCepDto enderecoDto = await apiCepTask;
                    return enderecoDto.MapearParaEndereco();
                }

                if (result == awesomeApiTask)
                {
                    EnderecoAwesomeApiDto enderecoDto = await awesomeApiTask;
                    return enderecoDto.MapearParaEndereco();
                }
            }

            return null;
        }
    }
}
