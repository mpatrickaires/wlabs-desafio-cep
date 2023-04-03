using WLabsDesafioCEP.Domain.Entities;
using WLabsDesafioCEP.Domain.ValueObjects;
using WLabsDesafioCEP.Infra.Data.Interfaces;
using WLabsDesafioCEP.Infra.Data.Repositories;

namespace WLabsDesafioCEP.Infra.Data.Tests.Repositories
{
    [TestFixture]
    public class EnderecoRepositoryTests
    {
        [Test]
        public async Task ObterEnderecoPeloCepAsync_EnderecoEncontradoEmCache_RetornaEndereco()
        {
            var esperado = new Endereco();

            var cepGateway = new Mock<ICepGateway>();

            var cacheGateway = new Mock<ICacheGateway>();
            cacheGateway.Setup(c => c.ObterDesserializadoAsync<Endereco>(It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(esperado);

            var enderecoRepository = new EnderecoRepository(cepGateway.Object, cacheGateway.Object);
            Endereco resultado = await enderecoRepository.ObterEnderecoPeloCepAsync(new Cep("58020782"));

            Assert.That(resultado, Is.EqualTo(esperado));
        }


        [Test]
        public async Task ObterEnderecoPeloCepAsync_NaoEncontrouEmCacheMasEncontrouNoGateway_RetornaEndereco()
        {
            var esperado = new Endereco();

            var cepGateway = new Mock<ICepGateway>();
            cepGateway.Setup(c => c.ObterEnderecoPeloCepAsync(It.IsAny<Cep>()))
                .ReturnsAsync(esperado);

            var cacheGateway = new Mock<ICacheGateway>();

            var enderecoRepository = new EnderecoRepository(cepGateway.Object, cacheGateway.Object);
            Endereco resultado = await enderecoRepository.ObterEnderecoPeloCepAsync(new Cep("58020782"));

            Assert.That(resultado, Is.EqualTo(esperado));
        }
    }
}
