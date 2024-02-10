using AutoMapper;
using Hangfire;
using lestoma.Api.Core;
using lestoma.CommonUtils.Constants;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Requests.Filters;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace lestoma.Api.Controllers
{
    [Route("api/reports-laboratory")]
    [ApiController]

    public class ReportsController : BaseController
    {
        private readonly IReporteService _reporteService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        #region Constructor
        public ReportsController(IMapper mapper, IDataProtectionProvider protectorProvider, IReporteService reporteService,
            IBackgroundJobClient backgroundJobClient)
            : base(mapper, protectorProvider)
        {
            _reporteService = reporteService;
            _backgroundJobClient = backgroundJobClient;
        }
        #endregion

        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        [HttpGet("get-daily-time")]
        public async Task<IActionResult> GetDailyReportTime()
        {
            var response = await _reporteService.GetDailyReportTime();
            return Ok(response);
        }

        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        [HttpPost("daily")]
        public IActionResult ReportDaily([FromBody] ReportDailyFilterRequest filtro)
        {
            // Zona 710 SA Pacific Standard Time (UTC-05:00) Bogota, Lima, Quito
            TimeZoneInfo tzone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");

            RecurringJob.AddOrUpdate<IReporteService>(Constants.KEY_REPORT_DAILY, servicio => servicio.GetDailyReport(),
                Cron.Daily(filtro.Hour, filtro.Minute), tzone);

            return Accepted(Responses.SetAcceptedResponse(null, "Se ha generado correctamente la tarea recurrente, revise el dashboard de Hangfire."));
        }
        [AuthorizeRoles(TipoRol.SuperAdministrador, TipoRol.Administrador)]
        [HttpPost("by-date")]
        public IActionResult ReportByDate([FromBody] ReportFilterRequest filtro)
        {
            var isSuper = IsSuperAdmin();
            if (!isSuper)
                filtro.UpaId = UpaId();

            var EmailDesencryptedUser = EmailDesencrypted();
            //var reporte = await _reporteService.GenerateReportByDate(filtro, isSuper);
            //await _reporteService.SendReportByFilter(EmailDesencryptedUser);
            var jobId = _backgroundJobClient.Schedule<IReporteService>(service =>
            service.GenerateReportByDate(filtro, isSuper, EmailDesencryptedUser), TimeSpan.FromSeconds(5));

            _backgroundJobClient.ContinueJobWith<IReporteService>(jobId, service => service.SendReportByFilter(EmailDesencryptedUser));

            return Accepted(Responses.SetAcceptedResponse(null, @"Generando el reporte..., se enviará un correo con el reporte.
                                                                  ¡Aviso! si no hay datos se enviará un correo indicandolo."));
        }

        [AuthorizeRoles(TipoRol.SuperAdministrador, TipoRol.Administrador)]
        [HttpPost("by-components")]
        public IActionResult ReportByComponents([FromBody] ReportComponentFilterRequest filtro)
        {
            var isSuper = IsSuperAdmin();
            if (!isSuper)
                filtro.Filtro.UpaId = UpaId();

            var EmailDesencryptedUser = EmailDesencrypted();
            var jobId = _backgroundJobClient.Schedule<IReporteService>(service =>
            service.GenerateReportByComponents(filtro, isSuper, EmailDesencryptedUser), TimeSpan.FromSeconds(5));

            _backgroundJobClient.ContinueJobWith<IReporteService>(jobId, service => service.SendReportByFilter(EmailDesencryptedUser));

            return Accepted(Responses.SetAcceptedResponse(null, @"Generando el reporte..., se enviará un correo con el reporte.
                                                                  ¡Aviso! si no hay datos se enviará un correo indicandolo."));
        }
    }
}
