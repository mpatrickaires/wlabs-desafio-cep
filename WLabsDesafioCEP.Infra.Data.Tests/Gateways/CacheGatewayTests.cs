using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text;
using System.Text.Json;
using WLabsDesafioCEP.Infra.Data.Gateways;
using WLabsDesafioCEP.Infra.Data.Tests.Common;

namespace WLabsDesafioCEP.Infra.Data.Tests.Gateways
{
    [TestFixture]
    public class CacheGatewayTests
    {
        [Test]
        public async Task ObterAsync_EncontrouValor_RetornaString()
        {
            var cacheService = new Mock<IDistributedCache>();
            cacheService.Setup(c => c.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Array.Empty<byte>());

            var cacheGateway = new CacheGateway(cacheService.Object);
            string? resultado = await cacheGateway.ObterAsync("");

            Assert.That(resultado, Is.TypeOf<string>());
        }

        [Test]
        public async Task ObterAsync_ErroDeConexaoComRedis_RetornaNull()
        {
            var cacheService = new Mock<IDistributedCache>();
            cacheService.Setup(c => c.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new RedisConnectionException(ConnectionFailureType.None, ""));

            var cacheGateway = new CacheGateway(cacheService.Object);
            string? resultado = await cacheGateway.ObterAsync("");

            Assert.That(resultado, Is.Null);
        }

        [Test]
        public async Task ObterDesserializadoAsync_EncontrouValor_RetornaInstanciaDoGenericInformado()
        {
            string json = JsonSerializer.Serialize(new ClassePlaceholder());

            var cacheService = new Mock<IDistributedCache>();
            cacheService.Setup(c => c.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Encoding.UTF8.GetBytes(json));

            var cacheGateway = new CacheGateway(cacheService.Object);
            ClassePlaceholder? resultado = await cacheGateway.ObterDesserializadoAsync<ClassePlaceholder>("");

            Assert.That(resultado, Is.TypeOf<ClassePlaceholder>());
        }

        [Test]
        public async Task ObterDesserializadoAsync_NaoEncontrouValor_RetornaNull()
        {
            var cacheService = new Mock<IDistributedCache>();
            cacheService.Setup(c => c.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((byte[])null);

            var cacheGateway = new CacheGateway(cacheService.Object);
            ClassePlaceholder? resultado = await cacheGateway.ObterDesserializadoAsync<ClassePlaceholder>("");

            Assert.That(resultado, Is.Null);
        }

        [Test]
        public async Task ObterDesserializadoAsync_ErroDeConexaoComRedis_RetornaNull()
        {
            var cacheService = new Mock<IDistributedCache>();
            cacheService.Setup(c => c.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new RedisConnectionException(ConnectionFailureType.None, ""));

            var cacheGateway = new CacheGateway(cacheService.Object);
            ClassePlaceholder? resultado = await cacheGateway.ObterDesserializadoAsync<ClassePlaceholder>("");

            Assert.That(resultado, Is.Null);
        }

        [Test]
        public async Task ObterDesserializadoAsync_ErroNaDesserializacao_RetornaNull()
        {
            var cacheService = new Mock<IDistributedCache>();
            cacheService.Setup(c => c.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                                .ReturnsAsync((byte[])null);

            var cacheGateway = new CacheGateway(cacheService.Object);
            ClassePlaceholder? resultado = await cacheGateway.ObterDesserializadoAsync<ClassePlaceholder>("");

            Assert.That(resultado, Is.Null);
        }
    }
}
