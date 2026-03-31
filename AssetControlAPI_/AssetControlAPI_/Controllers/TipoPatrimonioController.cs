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

        //[HttpGet]

        //[HttpGet("id/{guid}")]

        //[HttpGet("nome/{nome}")]
        //[HttpPost]
        //[HttpPut]

    }
}
