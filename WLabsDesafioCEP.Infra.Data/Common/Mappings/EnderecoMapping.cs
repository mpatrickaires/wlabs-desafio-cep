//using WLabsDesafioCEP.Domain.Entities;
//using WLabsDesafioCEP.Infra.Data.Common.Dtos;

//namespace WLabsDesafioCEP.Infra.Data.Common.Mappings
//{
//    public static class EnderecoMapping
//    {
//        public static Endereco MapearParaEndereco(this EnderecoApiCepDto enderecoDto) => new Endereco
//        {
//            Cep = enderecoDto.Code.Replace("-", ""),
//            Logradouro = enderecoDto.Address,
//            Bairro = enderecoDto.District,
//            Cidade = enderecoDto.City,
//            Estado = enderecoDto.State,
//        };

//        public static Endereco MapearParaEndereco(this EnderecoAwesomeApiDto enderecoDto) => new Endereco
//        {
//            Cep = enderecoDto.Cep,
//            Logradouro = enderecoDto.Address,
//            Bairro = enderecoDto.District,
//            Cidade = enderecoDto.City,
//            Estado = enderecoDto.State,
//        };

//        public static Endereco MapearParaEndereco(this EnderecoViaCepDto enderecoDto) => new Endereco
//        {
//            Cep = enderecoDto.Cep.Replace("-", ""),
//            Logradouro = enderecoDto.Logradouro,
//            Bairro = enderecoDto.Bairro,
//            Cidade = enderecoDto.Localidade,
//            Estado = enderecoDto.Uf,
//        };
//    }
//}
