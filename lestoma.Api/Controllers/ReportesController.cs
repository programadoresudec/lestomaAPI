using AutoMapper;
using Hangfire;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Requests;
using lestoma.Data;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace lestoma.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : BaseController
    {
        private readonly IReporteService _reporteService;
        #region Constructor
        public ReportesController(IMapper mapper, IReporteService reporteService)
            : base(mapper)
        {
            _reporteService = reporteService;
        }
        #endregion


        [HttpPost("daily")]
        public IActionResult ReportDaily([FromBody] FilterReportDailyRequest filtro)
        {
            RecurringJob.AddOrUpdate<IReporteService>("Enviar-reporte-diario", servicio => servicio.DailyReport(),
                Cron.Daily(filtro.Hour, filtro.Minute));

            return Ok(new Response
            {
                IsExito = true,
                Mensaje = "Se ha generado correctamente el job.",
                StatusCode = (int)HttpStatusCode.OK,
            });
        }


        [HttpPost("by-date")]
        public async Task<IActionResult> ReportByDate([FromBody] FilterReportRequest filtro)
        {
            var reporte = await _reporteService.ReportByDate(filtro, IsSuperAdmin());
            return File(reporte.ArchivoBytes,
               reporte.MIME, reporte.Archivo);
        }


        [HttpGet("by-components")]
        public async Task<IActionResult> ReportByComponents([FromBody] FilterReportComponentRequest filtro)
        {
            var reporte = await _reporteService.ReportByComponents(filtro, IsSuperAdmin());
            return File(reporte.ArchivoBytes,
            reporte.MIME, reporte.Archivo);
        }
    }
}
