using AssetControlAPI_.Applications.DTOs.AreaDTO;
using AssetControlAPI_.Applications.DTOs.BairroDTO;
using AssetControlAPI_.Applications.Services;
using AssetControlAPI_.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AssetControlAPI_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {

        private readonly AreaService _service;
        public AreaController(AreaService service) => _service = service;

        [HttpGet]
        public ActionResult<List<ListarAreaDTO>> Listar()
        {
            List<ListarAreaDTO> areas = _service.Listar();
            if (areas == null)
                return NotFound(areas);

            return Ok(areas);


        }

        [HttpGet("id/{id}")]

        public ActionResult<ListarAreaDTO> ObterGuid(Guid id)
        {
            try
            {
                ListarAreaDTO are = _service.ObterPorGuid(id);
                return Ok(are);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpGet("nome/{nome}")]
        public ActionResult<ListarAreaDTO> ObterPorNome(string nome)
        {
            try
            {
                ListarAreaDTO area = _service.ObterPorNome(nome);
                return Ok(nome);

            }

            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarAreaDTO areaDTO)
        {
            try
            {
                _service.Adicionar(areaDTO);
                return Created();
            }

            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("id/{guid}")]
        public ActionResult Atualizar(Guid guid, CriarAreaDTO areaDTO)
        {
            try
            {
                _service.Atualizar(guid, areaDTO);
                return NoContent();
            }

            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }

        }


    } 
}
