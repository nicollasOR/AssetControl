using AssetControlAPI_.Applications.DTOs.EndereçoDTO;
using AssetControlAPI_.Applications.Services;
using AssetControlAPI_.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetControlAPI_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {

        private readonly EnderecoService _service;

        public EnderecoController(EnderecoService service) => _service = service;

        [HttpGet]
        public ActionResult <List<LerEnderecoDTO>> Listar()
        {
            List<LerEnderecoDTO> lerDTO = _service.Listar();
            if (lerDTO == null)
                return NotFound(lerDTO);
            return Ok(lerDTO);
        }

        [HttpGet("id/{guid}")]
        public ActionResult <LerEnderecoDTO> BuscarPorId(Guid guid)
        {
            try
            {
                LerEnderecoDTO lerDTO = _service.BuscarPorId(guid);
                return Ok(lerDTO);
            }

            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("busca/")]
        public ActionResult<LerEnderecoDTO> BuscarPorLogradouroENumero(string logradouro, int? numero, Guid bairroId)
        {
            try
            {
                LerEnderecoDTO lerDTO = _service.BuscarPorLogradouroENumero(logradouro, numero, bairroId);
                return Ok(lerDTO);
            }

            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public ActionResult <CriarEnderecoDTO> Adicionar(CriarEnderecoDTO criarDTO)
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
        public ActionResult <LerEnderecoDTO> Atualizar(Guid id, CriarEnderecoDTO lerDTO)
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
