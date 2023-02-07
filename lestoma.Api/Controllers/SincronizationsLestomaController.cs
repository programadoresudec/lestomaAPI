using AutoMapper;
using Hangfire;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Requests;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lestoma.Api.Controllers
{
    [Route("api/sincronizaciones-lestoma")]
    [Authorize]
    [ApiController]
    public class SincronizationsLestomaController : BaseController
    {

        private readonly ILaboratorioService _laboratorioService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        #region Constructor
        public SincronizationsLestomaController(IMapper mapper, IDataProtectionProvider protectorProvider,
            ILaboratorioService laboratorioService, IBackgroundJobClient backgroundJobClient)
            : base(mapper, protectorProvider)
        {
            _laboratorioService = laboratorioService;
            _backgroundJobClient = backgroundJobClient;

        }
        #endregion


        [HttpGet("sync-data-online-to-database-device")]
        public async Task<IActionResult> SyncDeviceDatabase()
        {
            var upaId = UpaId();
            var data = await _laboratorioService.GetDataOfUserToSyncDeviceDatabase(upaId);
            return Ok(data);
        }


        [HttpPost("bulk-sync-data-offline")]
        public IActionResult SyncLabDataOffline(IEnumerable<LaboratorioRequestOffline> datosOfOffline)
        {
            var EmailDesencryptedUser = EmailDesencrypted();
            var datosMapeados = Mapear<IEnumerable<LaboratorioRequestOffline>, IEnumerable<ELaboratorio>>(datosOfOffline);
            var jobId = _backgroundJobClient.Schedule<ILaboratorioService>(service =>
           service.BulkSyncDataOffline(datosMapeados), TimeSpan.FromSeconds(10));
            _backgroundJobClient.ContinueJobWith<ILaboratorioService>(jobId, service => service.SendEmailFinishMerge(EmailDesencryptedUser));
            return Accepted(Responses.SetAcceptedResponse(null,
                "Se esta generando la migración de datos, pronto le llegará un correo cuando termine."));
        }
    }
}
