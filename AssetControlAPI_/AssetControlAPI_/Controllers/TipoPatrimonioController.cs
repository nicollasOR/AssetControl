using AssetControlAPI_.Applications.DTOs.TipoPatrimonioDTO;
using AssetControlAPI_.Applications.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetControlAPI_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPatrimonioController : ControllerBase
    {
        private readonly TipoPatrimonioService _service;

        public TipoPatrimonioController(TipoPatrimonioService service) => _service = service;

        [HttpGet]
        public ActionResult<List<ListarTipoPatrimonioDTO>> Listar()
        {
            List<ListarTipoPatrimonioDTO> dto = _service.Listar();
            if (dto == null)
                return NotFound(dto);
            return Ok(dto);

        }

        //[HttpGet("id/{guid}")]

        //[HttpGet("nome/{nome}")]
        //[HttpPost]
        //[HttpPut]

    }
}
