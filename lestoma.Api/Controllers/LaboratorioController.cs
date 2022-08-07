﻿using AutoMapper;
using Hangfire;
using lestoma.CommonUtils.Requests;
using lestoma.Data;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lestoma.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaboratorioController : BaseController
    {

        private readonly ILaboratorioService _laboratorioService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        #region Constructor
        public LaboratorioController(IMapper mapper, ILaboratorioService laboratorioService,
            IBackgroundJobClient backgroundJobClient, IDataProtectionProvider protectorProvider)
            : base(mapper, protectorProvider)
        {
            _laboratorioService = laboratorioService;
            _backgroundJobClient = backgroundJobClient;

        }
        #endregion

        [HttpGet("listado")]
        public async Task<IActionResult> GetDetalle()
        {
            return Ok();
        }

        
        [HttpPost("crear-detalle")]
        public async Task<IActionResult> MergeDetail(IEnumerable<LaboratorioRequestOffline> datosOfOffline)
        {
            var EmailDesencryptedUser = EmailDesencrypted();
            var datosMapeados = Mapear<IEnumerable<LaboratorioRequestOffline>, IEnumerable<ELaboratorio>>(datosOfOffline);
            var jobId = _backgroundJobClient.Schedule<ILaboratorioService>(service =>
           service.MergeDetails(datosMapeados), TimeSpan.FromSeconds(20));
            return Ok();
        }
    }
}
