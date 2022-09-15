using AutoMapper;
using Hangfire;
using lestoma.Api.Helpers;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Requests.Filters;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace lestoma.Api.Controllers
{
    [Route("api/reportes-laboratorio")]
    [ApiController]
 
    public class ReportesController : BaseController
    {
        private readonly IReporteService _reporteService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        #region Constructor
        public ReportesController(IMapper mapper, IReporteService reporteService,
            IBackgroundJobClient backgroundJobClient, IDataProtectionProvider protectorProvider)
            : base(mapper, protectorProvider)
        {
            _reporteService = reporteService;
            _backgroundJobClient = backgroundJobClient;
        }
        #endregion

        [Authorize(Roles = RolesEstaticos.SUPERADMIN)]
        [HttpPost("daily")]
        public IActionResult ReportDaily([FromBody] ReportDailyFilterRequest filtro)
        {
            RecurringJob.AddOrUpdate<IReporteService>("Enviar-reporte-diario", servicio => servicio.GetDailyReport(),
                Cron.Daily(filtro.Hour, filtro.Minute), TimeZoneInfo.Local);

            return Ok(new Response
            {
                IsExito = true,
                Mensaje = "Se ha generado correctamente el job.",
                StatusCode = (int)HttpStatusCode.OK,
            });
        }

        [Authorize(Roles = RolesEstaticos.SUPERADMIN + "," + RolesEstaticos.ADMIN)]
        [HttpPost("by-date")]
        public async Task<IActionResult> ReportByDate([FromBody] ReportFilterRequest filtro)
        {
            var isSuper = IsSuperAdmin();
            if (!isSuper)
            {
                filtro.UpaId = UpaId();
            }

            var obj = await _reporteService.GetReportByDate(filtro, isSuper);
            var jobId = _backgroundJobClient.Schedule<IReporteService>(service => service.GenerateReportByDate(obj.reporte, obj.archivo, filtro, isSuper),
                TimeSpan.FromSeconds(2));
            var EmailDesencryptedUser = EmailDesencrypted();
            _backgroundJobClient.ContinueJobWith<IReporteService>(jobId, service => service.SendReportByFilter(EmailDesencryptedUser));
            return Ok(new Response
            {
                Mensaje = "Se esta generando el reporte, pronto se enviará a su correo electrónico registrado.",
                IsExito = true,
                StatusCode = (int)HttpStatusCode.OK
            });

        }

        [Authorize(Roles = RolesEstaticos.SUPERADMIN + "," + RolesEstaticos.ADMIN)]
        [HttpPost("by-components")]
        public async Task<IActionResult> ReportByComponents([FromBody] ReportComponentFilterRequest filtro)
        {
            var isSuper = IsSuperAdmin();
            if (!isSuper)
            {
                filtro.Filtro.UpaId = UpaId();
            }
            var obj = await _reporteService.GetReportByComponents(filtro, isSuper);
            var jobId = _backgroundJobClient.Schedule<IReporteService>(service => service.GenerateReportByComponents(obj.reporte, obj.archivo, filtro, isSuper),
                TimeSpan.FromSeconds(2));
            var EmailDesencryptedUser = EmailDesencrypted();
            _backgroundJobClient.ContinueJobWith<IReporteService>(jobId, service => service.SendReportByFilter(EmailDesencryptedUser));
            return Ok(new Response
            {
                Mensaje = "Se esta generando el reporte, pronto se enviará a su correo electrónico registrado.",
                IsExito = true,
                StatusCode = (int)HttpStatusCode.OK
            });
        }
    }
}
