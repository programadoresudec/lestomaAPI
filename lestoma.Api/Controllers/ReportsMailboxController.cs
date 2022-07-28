using AutoMapper;
using lestoma.Api.Helpers;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Requests;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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

        [HttpGet("paginar")]
        public async Task<IActionResult> GetReportesBuzonPaginado([FromQuery] Paginacion paginacion)
        {
            var queryable = _buzonService.GetAllForPagination();
            var listado = await GetPaginacion<EBuzon, BuzonDTO>(paginacion, queryable);
            var paginador = Paginador<BuzonDTO>.CrearPaginador(queryable.Count(), listado, paginacion);
            return Ok(paginador);
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBuzonById(int id)
        {
            var buzon = await _buzonService.GetMailBoxById(id);
            var buzonDTO = Mapear<EBuzon, BuzonDTO>(buzon);
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
            Respuesta = await _buzonService.CreateMailBox(buzon);
            return Created(string.Empty, Respuesta);
        }
    }
}
