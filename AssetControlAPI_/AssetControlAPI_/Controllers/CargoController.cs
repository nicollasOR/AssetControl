using AssetControlAPI_.Applications.DTOs.CargoDTO;
using AssetControlAPI_.Applications.Services;
using AssetControlAPI_.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetControlAPI_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoController : ControllerBase
    {

        private readonly CargoService _service;

        public CargoController(CargoService service) => _service = service;

        [HttpGet]
        public ActionResult <List<ListarCargoDTO>> Listar()
        {
            List<ListarCargoDTO> listar = _service.Listar();
            if(listar == null)
                return NotFound(listar);

            return Ok(listar);
        }

        [HttpGet("id/{id}")]
        public ActionResult<ListarCargoDTO> ObterPorId(Guid id)
        {
            try
            {
                ListarCargoDTO listarDTO = _service.BuscarPorId(id);
                return Ok(listarDTO);
            }

            catch(DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("nome/{nome}")]
        public ActionResult<ListarCargoDTO> ObterPorNome(string nome)
        {
            try
            {
                ListarCargoDTO listarDTO = _service.BuscarPorNome(nome);
                return Ok(nome);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarCargoDTO cargoDTO)
        {
            try
            {
                _service.Adicionar(cargoDTO);
                return Created();
            }

            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("id/{guid}")]
        public ActionResult Atualizar(Guid guid, CriarCargoDTO cargoDTO)
        {
            try
            {
                _service.Atualizar(guid, cargoDTO);
                return NoContent();
            }

            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }

        }


    }
    }
