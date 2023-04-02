using WLabsDesafioCEP.Domain.Entities;
using WLabsDesafioCEP.Infra.Data.Interfaces;

namespace WLabsDesafioCEP.Infra.Data.Common.Dtos
{
    public class EnderecoApiCepDto : IMapeavelParaEndereco
    {
        public string Code { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string Address { get; set; }

        public int Status { get; set; }

        public bool Ok { get; set; }

        public string StatusText { get; set; }

        public Endereco MapearParaEndereco() => new Endereco
        {
            Cep = Code?.Replace("-", "") ?? null,
            Logradouro = Address,
            Bairro = District,
            Cidade = City,
            Estado = State,
        };
    }
}
