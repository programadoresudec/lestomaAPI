﻿using AutoMapper;
using DocumentFormat.OpenXml.Bibliography;
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
        public ReportsController(IMapper mapper, IReporteService reporteService,
            IBackgroundJobClient backgroundJobClient, IDataProtectionProvider protectorProvider)
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
            return Accepted(
                Responses.SetAcceptedResponse(null,
                "Se esta generando el reporte, pronto se enviará a su correo electrónico registrado."));
        }

        [AuthorizeRoles(TipoRol.SuperAdministrador, TipoRol.Administrador)]
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

            return Accepted(
             Responses.SetAcceptedResponse(null,
             "Se esta generando el reporte, pronto se enviará a su correo electrónico registrado."));
        }
    }
}