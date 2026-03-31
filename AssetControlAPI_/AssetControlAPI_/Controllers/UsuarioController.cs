using AssetControlAPI_.Applications.DTOs.UsuarioDTO;
using AssetControlAPI_.Applications.Services;
using AssetControlAPI_.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetControlAPI_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;
        public UsuarioController(UsuarioService service) => _service = service;

        [HttpGet]
        public ActionResult<List<ListarUsuarioDTO>> Listar()
        {
            List<ListarUsuarioDTO> usuarios = _service.Listar();
            if (usuarios == null)
                return BadRequest(usuarios);

            return Ok(usuarios);

        }

    }
}
