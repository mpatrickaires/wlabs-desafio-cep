using WLabsDesafioCEP.Domain.Exceptions;
using WLabsDesafioCEP.Domain.ValueObjects;

namespace WLabsDesafioCEP.Domain.Tests.ValueObjects
{
    [TestFixture]
    public class CepTests
    {
        [Test]
        public void Instanciar_ValorEhValido_RetornaCepComValor()
        {
            string valor = "58020782";
            
            var cep = new Cep(valor);

            Assert.That(cep.Valor, Is.EqualTo(valor));
        }

        [Test]
        public void Instanciar_ValorEhValidoEPossuiSeparador_RetornaCepComValorSemSeparador()
        {
            string valorComSeparador = "58020-782";
            string valorSemSeparador = valorComSeparador.Replace("-", "");
            
            var cep = new Cep(valorComSeparador);

            Assert.That(cep.Valor, Is.EqualTo(valorSemSeparador));
        }

        [Test]
        public void Instanciar_ValorEhNull_LancaCepInvalidoException()
        {
            Assert.Throws<CepInvalidoException>(() => new Cep(null));
        }

        [Test]
        public void Instanciar_ValorNaoContemApenasDigitos_LancaCepInvalidoException()
        {
            Assert.Throws<CepInvalidoException>(() => new Cep("abc"));
        }

        [Test]
        public void Instanciar_ValorTamanhoInvalido_LancaCepInvalidoException()
        {
            Assert.Throws<CepInvalidoException>(() => new Cep("0"));
        }
    }
}
