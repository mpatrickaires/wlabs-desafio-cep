using WLabsDesafioCEP.Domain.Entities;
using WLabsDesafioCEP.Infra.Data.Interfaces;

namespace WLabsDesafioCEP.Infra.Data.Common.Dtos
{
    public class EnderecoAwesomeApiDto : IMapeavelParaEndereco
    {
        public string Cep { get; set; }

        public string AddressType { get; set; }

        public string AddressName { get; set; }

        public string Address { get; set; }

        public string State { get; set; }

        public string District { get; set; }

        public string Lat { get; set; }

        public string Lng { get; set; }

        public string City { get; set; }

        public string CityIbge { get; set; }

        public string Ddd { get; set; }

        public Endereco MapearParaEndereco() => new Endereco
        {
            Cep = Cep,
            Logradouro = Address,
            Bairro = District,
            Cidade = City,
            Estado = State,
        };
    }
}
