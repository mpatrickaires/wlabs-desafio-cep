using Microsoft.AspNetCore.Mvc;
using WLabsDesafioCEP.Application.Interfaces;

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
        public async Task<IActionResult> ObterEnderecoPeloCepAsync(string cep)
        {
            return Ok(await _enderecoService.ObterEnderecoPeloCepAsync(cep));
        }
    }
}
