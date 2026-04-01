
using AssetControlAPI_.Applications.Services;
using Microsoft.AspNetCore.Http;
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
        public ActionResult <List<ListarTipoUsuario>> Listar()
        {
            List<ListarTipoUsuario> listar = _service.Listar();
            if(listar == null)
                return NotFound(listar);

                return Ok(listar);
        }

        [HttpGet("id/{guid}")]
        public ActionResult <ListarTipoUsuario> BuscarPorId(Guid guid)
        {
            try
            {
                ListarTipoUsuario listar = _service.ObterPorGuid(guid);
                return Ok(listar);
            }

            catch(DomainException ex)
            {
              return BadRequest(ex.Message);
            }

        }

        [HttpGet("nome/{nome}")]
        public ActionResult <ListarTipoUsuario> BuscarPorNome(string nome)
        {
            try
            {
                ListarTipoUsuario listar = _service.ObterPorNome(nome);
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
        public ActionResult <ListarTipoUsuario> Adicionar(Guid id, ListarTipoUsuario listarDTO)
        {
            try
            {
                _service.Adicionar(id, listarDTO);
                return NoContent();
            }

            catch(DomainException ex)
            {
              return BadRequest(ex.Message);
            }
        }


    }
}
