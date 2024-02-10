using AutoMapper;
using lestoma.Api.Core;
using lestoma.Api.Helpers;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.MyException;
using lestoma.CommonUtils.Requests;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace lestoma.Api.Controllers
{
    [Route("api/buzon-de-reportes")]
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

        [AuthorizeRoles(TipoRol.SuperAdministrador, TipoRol.Administrador)]
        [HttpGet("paginar")]
        public async Task<IActionResult> GetReportesBuzonPaginado([FromQuery] Paginacion paginacion)
        {
            var queryable = _buzonService.GetAllForPagination(UpaId());
            var listado = await queryable.Paginar(paginacion).ToListAsync();
            var paginador = Paginador<BuzonDTO>.CrearPaginador(queryable.Count(), listado, paginacion);
            return Ok(paginador);
        }


        [HttpGet("info/{id}")]
        [AuthorizeRoles(TipoRol.SuperAdministrador, TipoRol.Administrador)]
        public async Task<IActionResult> GetBuzonById(int id)
        {
            var buzonInfo = await _buzonService.GetMailBoxById(id);
            return Ok(buzonInfo);
        }

        [HttpPost("create")]
        [AuthorizeRoles(TipoRol.SuperAdministrador, TipoRol.Administrador, TipoRol.Auxiliar)]
        public async Task<IActionResult> CrearReporteDelBuzon(BuzonCreacionRequest buzon)
        {
            try
            {
                if (!string.IsNullOrEmpty(buzon.Extension))
                {
                    buzon.Detalle.PathImagen = await _almacenadorArchivos.GuardarArchivo(buzon.Imagen, buzon.Extension, contenedor);
                }
                Respuesta = await _buzonService.CreateMailBox(buzon);
                return Created(string.Empty, Respuesta);

            }
            catch (Exception ex)
            {
                await _almacenadorArchivos.BorrarArchivo(buzon.Detalle.PathImagen, contenedor);
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, ex);
            }

        }
        [HttpPut("edit-status")]
        [AuthorizeRoles(TipoRol.SuperAdministrador, TipoRol.Administrador)]
        public async Task<IActionResult> EditStatus(EditarEstadoBuzonRequest buzon)
        {
            Respuesta = await _buzonService.EditStatusMailBox(buzon);
            return Ok(Respuesta);
        }
    }
}
