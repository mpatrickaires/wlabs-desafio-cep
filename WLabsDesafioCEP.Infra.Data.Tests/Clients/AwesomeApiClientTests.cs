using Refit;
using System.Net;
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
    public class AwesomeApiClientTests
    {
        [Test]
        public async Task ObterEnderecoPeloCepAsync_ExecutouNormal_RetornaEnderecoAwesomeApiDto()
        {
            var esperado = new EnderecoAwesomeApiDto();

            var awesomeApiRefit = new Mock<IAwesomeApiRefit>();
            awesomeApiRefit.Setup(a => a.ObterEnderecoPeloCepAsync(It.IsAny<string>()))
                .ReturnsAsync(esperado);

            var awesomeApiClient = new AwesomeApiClient(awesomeApiRefit.Object);
            IMapeavelParaEndereco resultado = await awesomeApiClient.ObterEnderecoPeloCepAsync(new Cep(CepUtils.ValorCepValido()));

            Assert.That(resultado, Is.EqualTo(esperado));
        }

        [Test]
        public async Task ObterEnderecoPeloCepAsync_OcorreuApiExceptionComStatusCodeNotFound_LancaCepInexistenteException()
        {
            var awesomeApiRefit = new Mock<IAwesomeApiRefit>();
            ApiException apiException = await ApiException
                    .Create(null, null, new HttpResponseMessage(HttpStatusCode.NotFound), null);
            awesomeApiRefit.Setup(a => a.ObterEnderecoPeloCepAsync(It.IsAny<string>()))
                .ThrowsAsync(apiException);

            var awesomeApiClient = new AwesomeApiClient(awesomeApiRefit.Object);

            Assert.ThrowsAsync<CepInexistenteException>(() => awesomeApiClient.ObterEnderecoPeloCepAsync(new Cep(CepUtils.ValorCepValido())));
        }
    }
}
