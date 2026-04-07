using AssetControlAPI_.Applications.DTOs.AutenticacaoDTO;
using AssetControlAPI_.Applications.Services;
using AssetControlAPI_.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AssetControlAPI_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {

        private readonly AutenticacaoService _service;
        public AutenticacaoController(AutenticacaoService service) => _service = service;

        [HttpPost("login")]
        public ActionResult<TokenDTO> Login(LoginDTO loginDTO)
        {
            try
            {
                TokenDTO token = _service.Login(loginDTO);
                return Ok(token);
            }
            catch(DomainException ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        [Authorize]
        [HttpPatch("trocar_senha")]
        public ActionResult TrocarPrimeiraSenhaDTO(TrocarPrimeiraSenha dto)
        {
            try
            {
                string usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(usuarioIdClaim))
                    return Unauthorized(usuarioIdClaim);

                //convertendo string para guid
                Guid usuarioId = Guid.Parse(usuarioIdClaim);
                _service.TrocarPrimeiraSenha(usuarioId, dto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
