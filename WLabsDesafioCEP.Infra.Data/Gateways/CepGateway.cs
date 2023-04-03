using WLabsDesafioCEP.Domain.Entities;
using WLabsDesafioCEP.Domain.Exceptions;
using WLabsDesafioCEP.Domain.ValueObjects;
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
            Task<IMapeavelParaEndereco> viaCepTask = _viaCepClient.ObterEnderecoPeloCepAsync(cep);
            Task<IMapeavelParaEndereco> apiCepTask = _apiCepClient.ObterEnderecoPeloCepAsync(cep);
            Task<IMapeavelParaEndereco> awesomeApiTask = _awesomeApiClient.ObterEnderecoPeloCepAsync(cep);

            Endereco endereco = await AguardarTasks(viaCepTask, apiCepTask, awesomeApiTask);
            return endereco;
        }

        private async Task<Endereco> AguardarTasks(params Task<IMapeavelParaEndereco>[] tasks)
        {
            List<Task<IMapeavelParaEndereco>> listaTasks = tasks.ToList();

            while (listaTasks.Any())
            {
                Task<IMapeavelParaEndereco> task = await Task.WhenAny(listaTasks);
                try
                {
                    IMapeavelParaEndereco resultado = await task;
                    return resultado.MapearParaEndereco();
                }
                catch (Exception e)
                {
                    if (e is CepInexistenteException) throw;
                    listaTasks.Remove(task);
                }
            }

            throw new CepInexistenteException();
        }
    }
}
