using AutoMapper;
using lestoma.Api.Helpers;
using lestoma.CommonUtils.Entities;
using lestoma.CommonUtils.Requests;
using lestoma.CommonUtils.Responses;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lestoma.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsMailboxController : BaseController
    {
        private readonly IAlmacenadorArchivos _almacenadorArchivos;
        private readonly IBuzonService _buzonService;
        private readonly string contenedor = "ReportesDelBuzon";
        public ReportsMailboxController(IMapper mapper, IAlmacenadorArchivos almacenadorArchivos, IBuzonService buzonService)
            : base(mapper)
        {

            _buzonService = buzonService;
            _almacenadorArchivos = almacenadorArchivos;
        }

        [Authorize]
        [HttpGet("listado")]
        public async Task<IActionResult> Lista()
        {
            var lista = await _buzonService.Listado();
            var listadobuzonDTO = Mapear<List<EBuzon>, List<BuzonResponse>>(lista);
            return Ok(listadobuzonDTO);
        }


        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBuzonById(int id)
        {
            var buzon = await _buzonService.GetBuzonById(id);
            var buzonDTO = Mapear<EBuzon, BuzonResponse>(buzon);
            return Ok(buzonDTO);
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CrearReporteDelBuzon(BuzonCreacionRequest buzon)
        {
            if (!string.IsNullOrEmpty(buzon.Extension))
            {
                buzon.Detalle.PathImagen = await _almacenadorArchivos.GuardarArchivo(buzon.Imagen, buzon.Extension, contenedor);
            }
            Respuesta = await _buzonService.AgregarReporte(buzon);
            return Created(string.Empty, Respuesta);
        }
    }
}
