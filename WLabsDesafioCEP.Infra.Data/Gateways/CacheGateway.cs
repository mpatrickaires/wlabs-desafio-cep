using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text.Json;
using WLabsDesafioCEP.Infra.Data.Interfaces;

namespace WLabsDesafioCEP.Infra.Data.Gateways
{
    public class CacheGateway : ICacheGateway
    {
        private readonly IDistributedCache _cacheClient;

        public CacheGateway(IDistributedCache cacheClient)
        {
            _cacheClient = cacheClient;
        }

        public async Task<string?> ObterAsync(string chave)
        {
            try
            {
                string? valorCache = await _cacheClient.GetStringAsync(chave);
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
                string? valorString = await _cacheClient.GetStringAsync(chave);

                if (valorString == null) return null;

                try
                {
                    T? valorDesserializado = JsonSerializer.Deserialize<T>(valorString);
                    return valorDesserializado;
                }
                catch (JsonException)
                {
                    if (removerSeDesserializacaoFalhar) _ = _cacheClient.RemoveAsync(chave);
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
                await _cacheClient.RemoveAsync(chave);
            }
            catch (RedisConnectionException)
            {
            }
        }

        public async Task SalvarAsync<T>(string chave, T valor)
        {
            try
            {
                await _cacheClient.SetStringAsync(chave, JsonSerializer.Serialize(valor));
            }
            catch (RedisConnectionException)
            {
            }
        }
    }
}
