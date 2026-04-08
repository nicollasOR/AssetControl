using AssetControlAPI_.Applications.DTOs.SolicitacaoTransferenciaDTO;
using AssetControlAPI_.Applications.Services;
using AssetControlAPI_.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetControlAPI_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitacaoTransferenciaController : ControllerBase
    {

        private readonly SolicitacaoTransferenciaService _service;
        public SolicitacaoTransferenciaController(SolicitacaoTransferenciaService service) => _service = service;

        [Authorize]
        [HttpGet]
        public ActionResult<List<ListarSolicitacaoTransferenciaDTO>> Listar()
        {
            List<ListarSolicitacaoTransferenciaDTO> listarDTO = _service.Listar();
            if (listarDTO == null)
                return NotFound(listarDTO);

            return Ok(listarDTO);
        }


        [Authorize]
        [HttpGet("id/{id}")]
        public ActionResult<ListarSolicitacaoTransferenciaDTO> BuscarPorId(Guid id)
        {
            try
            {
                ListarSolicitacaoTransferenciaDTO listarDTO = _service.BuscarPorId(id);
                return Ok(listarDTO);
            }

            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
