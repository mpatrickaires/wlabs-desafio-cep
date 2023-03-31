using WLabsDesafioCEP.Domain.Entities;
using WLabsDesafioCEP.Domain.Interfaces;
using WLabsDesafioCEP.Domain.ValueObjects;
using WLabsDesafioCEP.Infra.Data.Interfaces;

namespace WLabsDesafioCEP.Infra.Data.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly ICepGateway _cepGateway;
        private readonly ICacheGateway _cacheGateway;

        public EnderecoRepository(ICepGateway cepGateway, ICacheGateway cacheGateway)
        {
            _cepGateway = cepGateway;
            _cacheGateway = cacheGateway;
        }

        public async Task<Endereco> ObterEnderecoPeloCepAsync(Cep cep)
        {
            Endereco? enderecoCache = await _cacheGateway.ObterDesserializadoAsync<Endereco>(cep.Valor);

            if (enderecoCache != null) return enderecoCache;

            Endereco endereco = await _cepGateway.ObterEnderecoPeloCepAsync(cep);
            _ = _cacheGateway.SalvarAsync(cep.Valor, endereco);

            return endereco;
        }
    }
}
