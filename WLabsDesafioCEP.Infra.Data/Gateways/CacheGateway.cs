using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text.Json;
using WLabsDesafioCEP.Common.Interfaces;
using WLabsDesafioCEP.Infra.Data.Interfaces;

namespace WLabsDesafioCEP.Infra.Data.Gateways
{
    public class CacheGateway : ICacheGateway
    {
        private readonly IDistributedCache _cacheClient;
        private readonly ILoggerService _loggerService;

        public CacheGateway(IDistributedCache cacheClient, ILoggerService loggerService)
        {
            _cacheClient = cacheClient;
            _loggerService = loggerService;
        }

        public async Task<string?> ObterAsync(string chave)
        {
            try
            {
                string? valorCache = await _cacheClient.GetStringAsync(chave);
                return valorCache;
            }
            catch (RedisConnectionException e)
            {
                CriarLogErroConexaoRedis(e);
                return null;
            }
        }

        private void CriarLogErroConexaoRedis(RedisConnectionException e) => _loggerService
            .LogError("Erro de conexão com o Redis!", e.Message, e.StackTrace);

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
            catch (RedisConnectionException e)
            {
                CriarLogErroConexaoRedis(e);
                return null;
            }
        }

        public async Task RemoverAsync(string chave)
        {
            try
            {
                await _cacheClient.RemoveAsync(chave);
            }
            catch (RedisConnectionException e)
            {
                CriarLogErroConexaoRedis(e);
            }
        }

        public async Task SalvarAsync<T>(string chave, T valor)
        {
            try
            {
                await _cacheClient.SetStringAsync(chave, JsonSerializer.Serialize(valor));
            }
            catch (RedisConnectionException e)
            {
                CriarLogErroConexaoRedis(e);
            }
        }
    }
}
