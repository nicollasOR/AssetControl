using AssetControlAPI_.Applications.DTOs.TipoAlteracaoDTO;
using AssetControlAPI_.Applications.Services;
using AssetControlAPI_.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetControlAPI_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoAlteracaoController : ControllerBase
    {

        private readonly TipoAlteracaoService _service;
        public TipoAlteracaoController(TipoAlteracaoService service)
         =>   _service = service;

        [HttpGet]
        public ActionResult <List<ListarTipoAlteracaoDTO>> Listar()
        {
            List<ListarTipoAlteracaoDTO> listarDTO = _service.Listar();
            if (listarDTO == null)
                return NotFound(listarDTO);

                return Ok(listarDTO);
        }

        [HttpGet("id/{id}")]
        public ActionResult <ListarTipoAlteracaoDTO> BuscarPorId(Guid id)
        {
            try
            {
                ListarTipoAlteracaoDTO dto = _service.BuscarPorId(id);
                return Ok(id);
            }
            
            catch(DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("nome/{nome}")]
        public ActionResult<ListarTipoAlteracaoDTO> BuscarPorNome(string nome)
        {
            try
            {
                ListarTipoAlteracaoDTO listarDTO = _service.BuscarPorNome(nome);
                return Ok(listarDTO);
            }

            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<CriarTipoAlteracaoDTO> Adicionar(CriarTipoAlteracaoDTO criarDTO)
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
        public ActionResult<CriarTipoAlteracaoDTO> Atualizar(Guid id, CriarTipoAlteracaoDTO criarDTO)
        {
            try
            {
                _service.Atualizar(id, criarDTO);
                return NoContent();
            }

            catch (DomainException ex)
            {
                return BadRequest(ex.Message);

            }

        }
    }
}
