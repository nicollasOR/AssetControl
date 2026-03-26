using AssetControlAPI_.Applications.DTOs.LocalizacaoDTO;
using AssetControlAPI_.Applications.Services;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetControlAPI_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalizacaoController : ControllerBase
    {

        private readonly LocalizacaoService _service;

        public LocalizacaoController (LocalizacaoService service) => _service = service;

        [HttpGet]
        public ActionResult <List<ListarLocalizacaoDTO>> Listar()
        {
            List<ListarLocalizacaoDTO> listarDTO = _service.Listar();
            if(listarDTO == null) 
                return NotFound(listarDTO);

            return Ok(listarDTO);
        }

        [HttpGet("id/{guid}")]
        public ActionResult <ListarLocalizacaoDTO> ObterPorGuid(Guid guid)
        {
            try
            {
                ListarLocalizacaoDTO listarDTO = _service.ObterPorGuid(guid);
                return Ok(listarDTO);
            }

            catch(DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost("criar/")]
        public ActionResult Adicionar(CriarLocalizacaoDTO criarDTO)
        {
            try
            {
                _service.Adicionar(criarDTO);
                return Created();
            }

            catch(DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("atualizar/{guid}")]
        public ActionResult Atualizar(Guid guid, CriarLocalizacaoDTO localDTO)
        {
            try
            {
                _service.Atualizar(guid, localDTO);
                return NoContent();
            }

            catch(DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
