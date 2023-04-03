using Refit;
using System.Net;
using WLabsDesafioCEP.Domain.Exceptions;
using WLabsDesafioCEP.Domain.ValueObjects;
using WLabsDesafioCEP.Infra.Data.Clients;
using WLabsDesafioCEP.Infra.Data.Common.Dtos;
using WLabsDesafioCEP.Infra.Data.Interfaces;
using WLabsDesafioCEP.Infra.Data.Refit;

namespace WLabsDesafioCEP.Infra.Data.Tests.Clients
{
    [TestFixture]
    public class ApiCepClientTests
    {
        [Test]
        public async Task ObterEnderecoPeloCepAsync_ExecutouNormal_RetornaEnderecoApiCepDto()
        {
            var esperado = new EnderecoApiCepDto();

            var apiCepRefit = new Mock<IApiCepRefit>();
            apiCepRefit.Setup(a => a.ObterEnderecoPeloCepAsync(It.IsAny<string>()))
                .ReturnsAsync(esperado);

            var apiCepClient = new ApiCepClient(apiCepRefit.Object);
            IMapeavelParaEndereco resultado = await apiCepClient.ObterEnderecoPeloCepAsync(new Cep("58020782"));

            Assert.That(resultado, Is.EqualTo(esperado));
        }

        [Test]
        public async Task ObterEnderecoPeloCepAsync_OcorreuApiExceptionComStatusCodeNotFound_LancaCepInexistenteException()
        {
            var apiCepRefit = new Mock<IApiCepRefit>();
            ApiException apiException = await ApiException
                    .Create(null, null, new HttpResponseMessage(HttpStatusCode.NotFound), null);
            apiCepRefit.Setup(a => a.ObterEnderecoPeloCepAsync(It.IsAny<string>()))
                .ThrowsAsync(apiException);

            var apiCepClient = new ApiCepClient(apiCepRefit.Object);

            Assert.ThrowsAsync<CepInexistenteException>(() => apiCepClient.ObterEnderecoPeloCepAsync(new Cep("58020782")));
        }
    }
}
