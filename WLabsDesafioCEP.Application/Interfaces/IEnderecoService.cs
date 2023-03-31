using WLabsDesafioCEP.Application.Common.Dtos;

namespace WLabsDesafioCEP.Application.Interfaces
{
    public interface IEnderecoService
    {
        Task<EnderecoDto> ObterEnderecoPeloCepAsync(string numeroCep);
    }
}
