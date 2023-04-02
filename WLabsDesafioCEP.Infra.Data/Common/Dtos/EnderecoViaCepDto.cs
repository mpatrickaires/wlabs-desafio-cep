using WLabsDesafioCEP.Domain.Entities;
using WLabsDesafioCEP.Infra.Data.Interfaces;

namespace WLabsDesafioCEP.Infra.Data.Common.Dtos
{
    public class EnderecoViaCepDto : IMapeavelParaEndereco
    {
        public string Cep { get; set; }

        public string Logradouro { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string Localidade { get; set; }

        public string Uf { get; set; }

        public string Ibge { get; set; }

        public string Gia { get; set; }

        public string Ddd { get; set; }

        public string Siafi { get; set; }
        public bool Erro { get; set; }

        public Endereco MapearParaEndereco() => new Endereco
        {
            Cep = Cep?.Replace("-", "") ?? null,
            Logradouro = Logradouro,
            Bairro = Bairro,
            Cidade = Localidade,
            Estado = Uf,
        };
    }
}
