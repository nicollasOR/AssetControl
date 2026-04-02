using AssetControlAPI_.Applications.DTOs.UsuarioDTO;
using AssetControlAPI_.Applications.Services;
using AssetControlAPI_.Contexts;
using AssetControlAPI_.Exceptions;
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
                return NotFound(usuarios);

            return Ok(usuarios);
        }

        [HttpGet("id/{id}")]
        public ActionResult<ListarUsuarioDTO> BuscarPorId(Guid id)
        {

            try
            {
                ListarUsuarioDTO listar = _service.BuscarPorId(id);
                return Ok(listar);
            }

            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("nome/{nome}")]
        public ActionResult<ListarUsuarioDTO> BuscarPorNome(string nome)
        {
            try
            {
                ListarUsuarioDTO listar = _service.BuscarPorNome(nome);
                return Ok(listar);
            }

            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("NIF/{NIF}")]
        public ActionResult<ListarUsuarioDTO> BuscarPorNIF(string NIF)
        {
            try
            {
                ListarUsuarioDTO listar = _service.BuscarPorNIF(NIF);
                return Ok(listar);
            }

            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost]
        public ActionResult<CriarUsuarioDTO> Adicionar(CriarUsuarioDTO criarDTO)
        {
            try
            {
                _service.Adicionar(criarDTO);
                return Created();
            }

            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public ActionResult<ListarUsuarioDTO> Atualizar(Guid id, CriarUsuarioDTO lerDTO)
        {
            try
            {
                _service.Atualizar(id, lerDTO);
                return NoContent();
            }

            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
