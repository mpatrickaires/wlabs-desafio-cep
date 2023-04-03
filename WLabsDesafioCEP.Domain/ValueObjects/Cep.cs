using WLabsDesafioCEP.Domain.Exceptions;

namespace WLabsDesafioCEP.Domain.ValueObjects
{

    public class Cep
    {
        private const int TamanhoValido = 8;
        private const int PosicaoSeparador = 5;
        private const string Separador = "-";

        public string Valor { get; }
        public string ValorComSeparador => Valor.Insert(PosicaoSeparador, Separador);

        public Cep(string valor)
        {
            if (valor == null) throw new CepInvalidoException("O valor do CEP não pode ser null!");

            valor = valor.Replace("-", "");

            if (!valor.All(c => char.IsDigit(c)))
            {
                throw new CepInvalidoException($"O CEP deve conter apenas dígitos!");
            }

            if (valor.Length != TamanhoValido)
            {
                throw new CepInvalidoException($"O CEP deve conter exatamente {TamanhoValido} dígitos!");
            }

            Valor = valor;
        }
    }
}
