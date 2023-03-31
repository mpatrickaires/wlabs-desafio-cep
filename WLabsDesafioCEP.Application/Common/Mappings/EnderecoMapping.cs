using WLabsDesafioCEP.Application.Common.Dtos;
using WLabsDesafioCEP.Domain.Entities;

namespace WLabsDesafioCEP.Application.Comum.Mappings
{
    public static class EnderecoMapping
    {
        public static EnderecoDto MapearParaEnderecoDto(this Endereco endereco) => new EnderecoDto
        {
            Cep = endereco.Cep,
            Cidade = endereco.Cidade,
            Estado = endereco.Estado,
            Logradouro = endereco.Logradouro,
            Bairro = endereco.Bairro,
        };
    }
}
