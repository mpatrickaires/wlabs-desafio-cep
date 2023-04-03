using WLabsDesafioCEP.Domain.Entities;
using WLabsDesafioCEP.Domain.Exceptions;
using WLabsDesafioCEP.Domain.ValueObjects;
using WLabsDesafioCEP.Infra.Data.Gateways;
using WLabsDesafioCEP.Infra.Data.Interfaces;

namespace WLabsDesafioCEP.Infra.Data.Tests.Gateways
{
    [TestFixture]
    public class CepGatewayTests
    {
        [Test]
        public async Task ObterEnderecoPeloCepAsync_AlgumClientRetornouEndereco_RetornaEndereco()
        {
            var mapeavelParaEndereco = new Mock<IMapeavelParaEndereco>();
            mapeavelParaEndereco.Setup(m => m.MapearParaEndereco())
                .Returns(new Endereco());

            void SetupRetornarEndereco<T>(Mock<T> mock) where T : class, ICepClient {
                mock.Setup(a => a.ObterEnderecoPeloCepAsync(It.IsAny<Cep>()))
                .ReturnsAsync(mapeavelParaEndereco.Object);
            }

            var apiCepClient = new Mock<IApiCepClient>();
            SetupRetornarEndereco(apiCepClient);

            var awesomeApiClient = new Mock<IAwesomeApiClient>();
            SetupRetornarEndereco(awesomeApiClient);

            var viaCepClient = new Mock<IViaCepClient>();
            SetupRetornarEndereco(viaCepClient);

            var cepGateway = new CepGateway(apiCepClient.Object, awesomeApiClient.Object, viaCepClient.Object);
            Endereco resultado = await cepGateway.ObterEnderecoPeloCepAsync(new Cep("58020782"));

            Assert.That(resultado, Is.TypeOf<Endereco>());
        }

        [Test]
        public async Task ObterEnderecoPeloCepAsync_AlgumClientLancouCepInexistenteException_LancaCepInexistenteException()
        {
            var mapeavelParaEndereco = new Mock<IMapeavelParaEndereco>();
            mapeavelParaEndereco.Setup(m => m.MapearParaEndereco())
                .Returns(new Endereco());

            void SetupLancarCepInexistenteException<T>(Mock<T> mock) where T : class, ICepClient
            {
                mock.Setup(a => a.ObterEnderecoPeloCepAsync(It.IsAny<Cep>()))
                .ThrowsAsync(new CepInexistenteException());
            }

            var apiCepClient = new Mock<IApiCepClient>();
            SetupLancarCepInexistenteException(apiCepClient);

            var awesomeApiClient = new Mock<IAwesomeApiClient>();
            SetupLancarCepInexistenteException(awesomeApiClient);

            var viaCepClient = new Mock<IViaCepClient>();
            SetupLancarCepInexistenteException(viaCepClient);

            var cepGateway = new CepGateway(apiCepClient.Object, awesomeApiClient.Object, viaCepClient.Object);

            Assert.ThrowsAsync<CepInexistenteException>(() => cepGateway.ObterEnderecoPeloCepAsync(new Cep("58020782")));
        }

        [Test]
        public async Task ObterEnderecoPeloCepAsync_TodosClientsLancaramException_LancaCepInexistenteException()
        {
            var mapeavelParaEndereco = new Mock<IMapeavelParaEndereco>();
            mapeavelParaEndereco.Setup(m => m.MapearParaEndereco())
                .Returns(new Endereco());

            void SetupLancarException<T>(Mock<T> mock) where T : class, ICepClient
            {
                mock.Setup(a => a.ObterEnderecoPeloCepAsync(It.IsAny<Cep>()))
                .ThrowsAsync(new Exception());
            }

            var apiCepClient = new Mock<IApiCepClient>();
            SetupLancarException(apiCepClient);

            var awesomeApiClient = new Mock<IAwesomeApiClient>();
            SetupLancarException(awesomeApiClient);

            var viaCepClient = new Mock<IViaCepClient>();
            SetupLancarException(viaCepClient);

            var cepGateway = new CepGateway(apiCepClient.Object, awesomeApiClient.Object, viaCepClient.Object);

            Assert.ThrowsAsync<CepInexistenteException>(() => cepGateway.ObterEnderecoPeloCepAsync(new Cep("58020782")));
        }
    }
}
