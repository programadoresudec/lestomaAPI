using AutoMapper;
using Hangfire;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Requests;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<IActionResult> ReportDaily([FromBody] FilterReportDailyRequest filtro)
        {
            var response = await _reporteService.DailyReport();

            RecurringJob.AddOrUpdate<IReporteService>("Enviar-reporte-diario", servicio => servicio.DailyReport(),
                Cron.Daily(filtro.Hour, filtro.Minute), TimeZoneInfo.Local);

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
            var isSuper = IsSuperAdmin();
            if (!isSuper)
            {
                filtro.UpaId = UpaId();
            }
            var reporte = await _reporteService.ReportByDate(filtro, isSuper);
            return File(reporte.ArchivoBytes,
               reporte.MIME, reporte.Archivo);
        }


        [HttpPost("by-components")]
        public async Task<IActionResult> ReportByComponents([FromBody] FilterReportComponentRequest filtro)
        {
            var isSuper = IsSuperAdmin();
            if (!isSuper)
            {
                filtro.Filtro.UpaId = UpaId();
            }
            var reporte = await _reporteService.ReportByComponents(filtro, isSuper);
            return File(reporte.ArchivoBytes,
            reporte.MIME, reporte.Archivo);
        }
    }
}
