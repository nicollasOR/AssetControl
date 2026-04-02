
using AssetControlAPI_.Applications.DTOs.TipoUsuarioDTO;
using AssetControlAPI_.Applications.Services;
using Microsoft.AspNetCore.Http;
using AssetControlAPI_.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AssetControlAPI_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        private readonly TipoUsuarioService _service;
        public TipoUsuarioController(TipoUsuarioService service) => _service = service;


        [HttpGet]
        public ActionResult <List<ListarTipoUsuarioDTO>> Listar()
        {
            List<ListarTipoUsuarioDTO> listar = _service.Listar();
            if(listar == null)
                return NotFound(listar);

                return Ok(listar);
        }

        [HttpGet("id/{guid}")]
        public ActionResult <ListarTipoUsuarioDTO> BuscarPorId(Guid guid)
        {
            try
            {
                ListarTipoUsuarioDTO listar = _service.ObterPorId(guid);
                return Ok(listar);
            }

            catch(DomainException ex)
            {
              return BadRequest(ex.Message);
            }

        }

        [HttpGet("nome/{nome}")]
        public ActionResult <ListarTipoUsuarioDTO> BuscarPorNome(string nome)
        {
            try
            {
                ListarTipoUsuarioDTO listar = _service.ObterPorNome(nome);
                return Ok(listar);
            }

            catch(DomainException ex)
            {
              return BadRequest(ex.Message);
            }
            
        }


        [HttpPost]
        public ActionResult <CriarTipoUsuarioDTO> Adicionar(CriarTipoUsuarioDTO criarDTO)
        {
            try
            {
                _service.Adicionar(criarDTO);
                return NoContent();
            }

            catch(DomainException ex)
            {
              return BadRequest(ex.Message);
            }
        }



        [HttpPut]
        public ActionResult <ListarTipoUsuarioDTO> Atualizar(Guid id, CriarTipoUsuarioDTO listarDTO)
        {
            try
            {
                _service.Atualizar(id, listarDTO);
                return NoContent();
            }

            catch(DomainException ex)
            {
              return BadRequest(ex.Message);
            }
        }


    }
}
