using Microsoft.AspNetCore.Mvc;
using WLabsDesafioCEP.Application.Common.Dtos;
using WLabsDesafioCEP.Application.Interfaces;
using WLabsDesafioCEP.WebAPI.Common.Dtos;

namespace WLabsDesafioCEP.WebAPI.Controllers
{
    [ApiController]
    [Route("cep")]
    [Produces("application/json")]
    public class CepController : ControllerBase
    {
        private readonly IEnderecoService _enderecoService;

        public CepController(IEnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        [HttpGet("{cep}")]
        public async Task<ActionResult<RespostaApiDto<EnderecoDto>>> ObterEnderecoPeloCepAsync(string cep)
        {
            return new RespostaApiDto<EnderecoDto>(await _enderecoService.ObterEnderecoPeloCepAsync(cep));
        }
    }
}
