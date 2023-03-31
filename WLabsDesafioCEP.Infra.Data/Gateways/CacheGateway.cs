using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
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

        public async Task<string?> ObterAsync(string chave)
        {
            try
            {
                string? valorCache = await _cacheService.GetStringAsync(chave);
                return valorCache;
            }
            catch (RedisConnectionException)
            {
                return null;
            }
        }

        public async Task<T?> ObterDesserializadoAsync<T>(string chave, bool removerSeDesserializacaoFalhar = true)
            where T : class
        {
            try
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
            catch (RedisConnectionException)
            {
                return null;
            }
        }

        public async Task RemoverAsync(string chave)
        {
            try
            {
                await _cacheService.RemoveAsync(chave);
            }
            catch (RedisConnectionException)
            {
            }
        }

        public async Task SalvarAsync<T>(string chave, T valor)
        {
            try
            {
                await _cacheService.SetStringAsync(chave, JsonSerializer.Serialize(valor));
            }
            catch (RedisConnectionException)
            {
            }
        }
    }
}
