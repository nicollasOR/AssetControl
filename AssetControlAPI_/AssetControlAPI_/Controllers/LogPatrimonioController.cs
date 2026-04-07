using AssetControlAPI_.Applications.DTOs.LogPatrimonioDTO;
using AssetControlAPI_.Applications.Services;
using AssetControlAPI_.Exceptions;
using AssetControlAPI_.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetControlAPI_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogPatrimonioController : ControllerBase
    {

        private readonly LogPatrimonioService _service;

        public LogPatrimonioController(LogPatrimonioService service) => _service = service;

        [HttpGet]
        public ActionResult<List<ListarLogPatrimonioDTO>> Listar()
        {
            List<ListarLogPatrimonioDTO> logs = _service.Listar();
            if(logs == null)
                return NotFound();
            return Ok(logs);
           
        }

        [Authorize(Roles = "Coordenador")]
        [HttpGet("patrimonio/{id}")]
        public ActionResult <List<ListarLogPatrimonioDTO>> BuscarPorPatrimonio(Guid id)
        {
            try
            {
                List<ListarLogPatrimonioDTO> logs = _service.BuscarPorPatrimonio(id);
                return Ok(logs);
            }

            catch (DomainException ex)
            {
                return BadRequest(ex.Message); 
            }
        }

    }
}
