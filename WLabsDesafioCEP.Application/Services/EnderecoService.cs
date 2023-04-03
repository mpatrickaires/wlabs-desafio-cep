using WLabsDesafioCEP.Application.Common.Dtos;
using WLabsDesafioCEP.Application.Comum.Mappings;
using WLabsDesafioCEP.Application.Exceptions;
using WLabsDesafioCEP.Application.Interfaces;
using WLabsDesafioCEP.Domain.Entities;
using WLabsDesafioCEP.Domain.Exceptions;
using WLabsDesafioCEP.Domain.Interfaces;
using WLabsDesafioCEP.Domain.ValueObjects;

namespace WLabsDesafioCEP.Application.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public EnderecoService(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        public async Task<EnderecoDto> ObterEnderecoPeloCepAsync(string numeroCep)
        {
            Cep cep;

            try
            {
                cep = new Cep(numeroCep);
            }
            catch (CepInvalidoException e)
            {
                throw new ValidacaoException(e.Message);
            }

            try
            {
                Endereco endereco = await _enderecoRepository.ObterEnderecoPeloCepAsync(cep);
                return endereco.MapearParaEnderecoDto();
            }
            catch (CepInexistenteException)
            {
                throw new NaoEncontradoException("Nenhum endereço encontrado para o CEP informado!");
            }
        }
    }
}
