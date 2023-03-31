using WLabsDesafioCEP.Domain.Entities;
using WLabsDesafioCEP.Domain.ValueObjects;

namespace WLabsDesafioCEP.Domain.Interfaces
{
    public interface IEnderecoRepository
    {
        Task<Endereco> ObterEnderecoPeloCepAsync(Cep cep);
    }
}
