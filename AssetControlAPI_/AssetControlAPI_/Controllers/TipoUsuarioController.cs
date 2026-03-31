
using AssetControlAPI_.Applications.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetControlAPI_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        private readonly TipoUsuarioService _service;
        public TipoUsuarioController(TipoUsuarioService service) => _service = service;


        //[HttpGet]

        //[HttpGet("id/{guid}")]

        //[HttpGet("nome/{nome}")]
        //[HttpPost]
        //[HttpPut]

    }
}
