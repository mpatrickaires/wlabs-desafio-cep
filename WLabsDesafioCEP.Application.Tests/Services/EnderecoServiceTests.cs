using WLabsDesafioCEP.Application.Common.Dtos;
using WLabsDesafioCEP.Application.Exceptions;
using WLabsDesafioCEP.Application.Services;
using WLabsDesafioCEP.Application.Tests.Common.Utils;
using WLabsDesafioCEP.Domain.Entities;
using WLabsDesafioCEP.Domain.Exceptions;
using WLabsDesafioCEP.Domain.Interfaces;
using WLabsDesafioCEP.Domain.ValueObjects;

namespace WLabsDesafioCEP.Application.Tests.Services
{
    [TestFixture]
    public class EnderecoServiceTests
    {

        [Test]
        public async Task ObterEnderecoPeloCepAsync_CepValidoETambemExiste_RetornaEnderecoDto()
        {
            var enderecoRepository = new Mock<IEnderecoRepository>();
            enderecoRepository.Setup(e => e.ObterEnderecoPeloCepAsync(It.IsAny<Cep>()))
                .ReturnsAsync(new Endereco());

            var enderecoService = new EnderecoService(enderecoRepository.Object);
            EnderecoDto resultado = await enderecoService.ObterEnderecoPeloCepAsync(CepUtils.NumeroCepValido);

            Assert.That(resultado, Is.InstanceOf<EnderecoDto>());
        }

        [Test]
        public void ObterEnderecoPeloCepAsync_CepInvalido_LancaValidacaoException()
        {
            var enderecoRepository = new Mock<IEnderecoRepository>();

            var enderecoService = new EnderecoService(enderecoRepository.Object);

            Assert.ThrowsAsync<ValidacaoException>(
                () => enderecoService.ObterEnderecoPeloCepAsync(CepUtils.NumeroCepInvalido));
        }

        [Test]
        public void ObterEnderecoPeloCepAsync_CepInexistente_LancaNaoEncontradoException()
        {
            var enderecoRepository = new Mock<IEnderecoRepository>();
            enderecoRepository.Setup(e => e.ObterEnderecoPeloCepAsync(It.IsAny<Cep>()))
                .ThrowsAsync(new CepInexistenteException());

            var enderecoService = new EnderecoService(enderecoRepository.Object);

            Assert.ThrowsAsync<NaoEncontradoException>(
                () => enderecoService.ObterEnderecoPeloCepAsync(CepUtils.NumeroCepValido));
        }

    }
}
