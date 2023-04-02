using WLabsDesafioCEP.Domain.Exceptions;
using WLabsDesafioCEP.Domain.ValueObjects;
using WLabsDesafioCEP.Infra.Data.Common.Dtos;
using WLabsDesafioCEP.Infra.Data.Interfaces;
using WLabsDesafioCEP.Infra.Data.Refit;

namespace WLabsDesafioCEP.Infra.Data.Clients
{
    public class ViaCepClient : IViaCepClient
    {
        private readonly IViaCepRefit _viaCepRefitClient;

        public ViaCepClient(IViaCepRefit viaCepRefitClient)
        {
            _viaCepRefitClient = viaCepRefitClient;
        }

        public async Task<IMapeavelParaEndereco> ObterEnderecoPeloCepAsync(Cep cep)
        {
            EnderecoViaCepDto enderecoDto = await _viaCepRefitClient.ObterEnderecoPeloCepAsync(cep.Valor);

            if (enderecoDto.Erro) throw new CepInexistenteException();

            return enderecoDto;
        }
    }
}
