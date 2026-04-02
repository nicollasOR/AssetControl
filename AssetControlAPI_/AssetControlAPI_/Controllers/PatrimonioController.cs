using AssetControlAPI_.Applications.DTOs.EndereçoDTO;
using AssetControlAPI_.Applications.DTOs.PatrimonioDTO;
using AssetControlAPI_.Applications.Services;
using AssetControlAPI_.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetControlAPI_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatrimonioController : ControllerBase
    {
        private readonly PatrimonioService _service;
        public PatrimonioController(PatrimonioService service) => _service = service;


        [HttpGet]
        public ActionResult <List<ListarPatrimonioDTO>> Listar()
        {
            List<ListarPatrimonioDTO> listar = _service.Listar();
                if (listar == null)
                return NotFound(listar);

            return Ok(listar);
        }

        [HttpGet("id/{id}")]
        public ActionResult<ListarPatrimonioDTO> BuscarPorId(Guid id)
        {
            try
            {
                ListarPatrimonioDTO dto = _service.BuscarPorId(id);
                return Ok(dto);
            }

            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("id/{numeroPatrimonio}/{patrimonioId?}")]
        public ActionResult<ListarPatrimonioDTO> BuscarPorNumeroPatrimonio(string numeroPatrimonio, Guid? patrimonioId = null)
        {
            try
            {
                ListarPatrimonioDTO dto = _service.BuscarPorNumeroPatrimonio(numeroPatrimonio, patrimonioId);
                return Ok(dto);
            }

            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<CriarPatrimonioDTO> Adicionar(ListarPatrimonioDTO criarDTO)
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
        public ActionResult<ListarPatrimonioDTO> Atualizar(Guid id, CriarPatrimonioDTO lerDTO)
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


        [HttpPatch("tipoPatrimonio/{id}")]
        public ActionResult <AtualizarStatusPatrimonioDTO> AtualizarStatus(Guid id, AtualizarStatusPatrimonioDTO criarDTO)
        {
            try
            {
                _service.AtualizarStatus(id, criarDTO);
                return NoContent();
            }

            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
