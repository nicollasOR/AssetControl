using AssetControlAPI_.Applications.DTOs.AreaDTO;
using AssetControlAPI_.Applications.DTOs.BairroDTO;
using AssetControlAPI_.Applications.Services;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetControlAPI_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BairroController : ControllerBase
    {

        private readonly BairroService _service;

        public BairroController(BairroService service) => _service = service;

        [HttpGet]
        public ActionResult<List<ListarBairroDTO>> Listar()
        {
            List<ListarBairroDTO> areaDTO = _service.Listar();
            if (areaDTO == null)
                return NotFound(areaDTO);

            return areaDTO;
        }

        [HttpGet("guid/{guid}")]
        public ActionResult <ListarBairroDTO> ObterPorGuid(Guid guid)
        {
            try
            {
                ListarBairroDTO bairro = _service.ObterPorGuid(guid);
                return Ok(bairro);
            }

            catch(DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarBairroDTO bairroDTO)
        {
            try
            {
                _service.Adicionar(bairroDTO);
                return Created();
            }
            catch(DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("id/{guid}")]
        public ActionResult Atualizar(Guid guid, CriarBairroDTO bairroDTO)
        {
            try
            {
                _service.Atualizar(guid, bairroDTO);
                return NoContent();
            }

            catch(DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
