using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using WLabsDesafioCEP.Infra.Data.Interfaces;

namespace WLabsDesafioCEP.Infra.Data.Gateways
{
    public class CacheGateway : ICacheGateway
    {
        private readonly IDistributedCache _cacheService;

        public CacheGateway(IDistributedCache cacheService)
        {
            _cacheService = cacheService;
        }

        public Task<string?> ObterAsync(string chave)
        {
            return _cacheService.GetStringAsync(chave);
        }

        public async Task<T?> ObterDesserializadoAsync<T>(string chave, bool removerSeDesserializacaoFalhar = true) 
            where T : class
        {
            string? valorString = await _cacheService.GetStringAsync(chave);

            if (valorString == null) return null;

            try
            {
                T? valorDesserializado = JsonSerializer.Deserialize<T>(valorString);
                return valorDesserializado;
            }
            catch (JsonException)
            {
                if (removerSeDesserializacaoFalhar) _ = _cacheService.RemoveAsync(chave);
                return null;
            }
        }

        public async Task RemoverAsync(string chave)
        {
            await _cacheService.RemoveAsync(chave);
        }

        public async Task SalvarAsync<T>(string chave, T valor)
        {
            await _cacheService.SetStringAsync(chave, JsonSerializer.Serialize(valor));
        }
    }
}
