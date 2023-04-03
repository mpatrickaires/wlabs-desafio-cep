using WLabsDesafioCEP.Common.Tests.Utils;
using WLabsDesafioCEP.Domain.Exceptions;
using WLabsDesafioCEP.Domain.ValueObjects;
using WLabsDesafioCEP.Infra.Data.Clients;
using WLabsDesafioCEP.Infra.Data.Common.Dtos;
using WLabsDesafioCEP.Infra.Data.Interfaces;
using WLabsDesafioCEP.Infra.Data.Refit;

namespace WLabsDesafioCEP.Infra.Data.Tests.Clients
{
    [TestFixture]
    public class ViaCepClientTests
    {
        [Test]
        public async Task ObterEnderecoPeloCepAsync_ExecutouNormal_RetornaViaCepDto()
        {
            var esperado = new EnderecoViaCepDto();

            var viaCepRefit = new Mock<IViaCepRefit>();
            viaCepRefit.Setup(a => a.ObterEnderecoPeloCepAsync(It.IsAny<string>()))
                .ReturnsAsync(esperado);

            var viaCepClient = new ViaCepClient(viaCepRefit.Object);
            IMapeavelParaEndereco resultado = await viaCepClient.ObterEnderecoPeloCepAsync(new Cep(CepUtils.ValorCepValido()));

            Assert.That(resultado, Is.EqualTo(esperado));
        }

        [Test]
        public async Task ObterEnderecoPeloCepAsync_SeRetornoEstaComErroTrue_LancaCepInexistenteException()
        {
            var viaCepRefit = new Mock<IViaCepRefit>();
            viaCepRefit.Setup(a => a.ObterEnderecoPeloCepAsync(It.IsAny<string>()))
                .ReturnsAsync(new EnderecoViaCepDto { Erro = true });

            var viaCepClient = new ViaCepClient(viaCepRefit.Object);

            Assert.ThrowsAsync<CepInexistenteException>(() => viaCepClient.ObterEnderecoPeloCepAsync(new Cep(CepUtils.ValorCepValido())));
        }
    }
}
