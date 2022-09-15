using lestoma.CommonUtils.Constants;
using lestoma.CommonUtils.Core;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Interfaces;
using lestoma.CommonUtils.MyException;
using lestoma.CommonUtils.Requests.Filters;
using lestoma.Data.Repositories;
using lestoma.Logica.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace lestoma.Logica.LogicaService
{
    public class ReporteService : IReporteService
    {
        private readonly ILoggerManager _logger;
        private IMemoryCache _cache;
        private readonly ReporteRepository _repositorio;
        private readonly IMailHelper _mailHelper;
        private readonly UpaRepository _upaRepository;
        private readonly ComponenteRepository _componenteRepository;
        private readonly IGenerateReport _generateReports;
        public ReporteService(ReporteRepository reporteRepository, IMailHelper mailHelper, ILoggerManager logger, IMemoryCache memoryCache,
            IGenerateReport generateReports, UpaRepository upaRepository, ComponenteRepository componenteRepository)
        {
            _cache = memoryCache;
            _logger = logger;
            _upaRepository = upaRepository;
            _repositorio = reporteRepository;
            _mailHelper = mailHelper;
            _generateReports = generateReports;
            _componenteRepository = componenteRepository;
        }
        public async Task<Response> DailyReport()
        {
            var filtro = new DateFilterRequest
            {
                FechaInicial = DateTime.Now.Date
            };
            filtro.FechaFinal = filtro.FechaInicial.AddDays(1).AddSeconds(-1);
            var listado = await _repositorio.DailyReport(filtro);
            if (listado.Reporte.Count == 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, @"No hay datos para generar el reporte diario.");
            }
            listado.FiltroFecha = filtro;
            return await GenerateDailyReport(listado);
        }

        public async Task<(byte[] ArchivoBytes, string MIME, string Archivo)> ReportByComponents(ReportComponentFilterRequest filtro,
            bool isSuperAdmin)
        {
            var file = GetFile(filtro.Filtro.TipoFormato);
            await ExistUpa(filtro.Filtro.UpaId);

            foreach (var item in filtro.ComponentesId)
            {
                bool existe = await _componenteRepository.AnyWithCondition(x => x.Id == item);
                if (!existe)
                {
                    throw new HttpStatusCodeException(HttpStatusCode.NotFound, $"Error: No se pudo encontrar el componente con el id: {item}");
                }
            }
            var listado = await _repositorio.ReportByComponents(filtro);
            if (listado.Reporte.Count == 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, @"No hay datos para generar el reporte.");
            }
            var bytes = _generateReports.GenerateReportByFormat(filtro.Filtro.TipoFormato, listado, isSuperAdmin);

            ArchivoDTO archivo = new()
            {
                ArchivoBytes = bytes,
                FileName = file.FILENAME,
                MIME = file.MIME
            };
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal);
            _cache.Set(Constants.CACHE_REPORTE_KEY, archivo, cacheEntryOptions);

            return (bytes, file.MIME, file.FILENAME);
        }
        public async Task<(byte[] ArchivoBytes, string MIME, string Archivo)> ReportByDate(ReportFilterRequest filtro, bool isSuperAdmin)
        {
            var file = GetFile(filtro.TipoFormato);
            await ExistUpa(filtro.UpaId);
            var listado = await _repositorio.ReportByDate(filtro);
            if (listado.Reporte.Count == 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, @"No hay datos para generar el reporte.");
            }
            listado.FiltroFecha = new DateFilterRequest()
            {
                FechaInicial = filtro.FechaInicial,
                FechaFinal = filtro.FechaFinal
            };

            var bytes = _generateReports.GenerateReportByFormat(filtro.TipoFormato, listado, isSuperAdmin);

            ArchivoDTO archivo = new()
            {
                ArchivoBytes = bytes,
                FileName = file.FILENAME,
                MIME = file.MIME
            };
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal);
            _cache.Set(Constants.CACHE_REPORTE_KEY, archivo, cacheEntryOptions);
            return (bytes, file.MIME, file.FILENAME);
        }

        private async Task ExistUpa(Guid upaId)
        {
            if (upaId != Guid.Empty)
            {
                var existe = await _upaRepository.AnyWithCondition(x => x.Id == upaId);
                if (!existe)
                    throw new HttpStatusCodeException(HttpStatusCode.NotFound, @$"Error: No se pudo encontrar la upa indicada.");
            }
        }

        private static (string MIME, string FILENAME) GetFile(GrupoTipoArchivo grupoTipoArchivo)
        {
            string dateString = DateTime.Now.ToString("yyyy-MM-dd");
            string Mime = string.Empty;
            string FileName = string.Empty;
            switch (grupoTipoArchivo)
            {
                case GrupoTipoArchivo.Imagen:
                    throw new HttpStatusCodeException(HttpStatusCode.BadRequest, @"Formato invalido, solo se puede PDF y EXCEL");
                case GrupoTipoArchivo.PDF:
                    Mime = MediaTypeNames.Application.Pdf;
                    FileName = $"Reporte_{dateString}.pdf";
                    break;
                case GrupoTipoArchivo.EXCEL:
                    Mime = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    FileName = $"Reporte_{dateString}.xlsx";
                    break;
            }
            return (Mime, FileName);
        }
        private async Task<Response> GenerateDailyReport(ReporteDTO reporte)
        {
            List<ArchivoDTO> archivos = new List<ArchivoDTO>();
            Response response = new Response();
            var pdf = _generateReports.GeneratePdf(reporte, true);
            var filePdf = GetFile(GrupoTipoArchivo.PDF);
            if (pdf != null)
            {
                archivos.Add(new ArchivoDTO()
                {
                    ArchivoBytes = pdf,
                    FileName = filePdf.FILENAME,
                    MIME = filePdf.MIME
                });
            }
            var excel = _generateReports.GenerateExcel(reporte, true);
            var fileExcel = GetFile(GrupoTipoArchivo.EXCEL);
            if (excel != null)
            {
                archivos.Add(new ArchivoDTO()
                {
                    ArchivoBytes = excel,
                    FileName = fileExcel.FILENAME,
                    MIME = fileExcel.MIME
                });
            }
            var correosSuperAdmin = await _repositorio.GetCorreosRolSuperAdmin();
            if (correosSuperAdmin.Count == 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, @$"Error: No se pudo encontrar correos destinatarios para super 
                                                                              administradores.");
            }
            foreach (var item in correosSuperAdmin)
            {
                await _mailHelper.SendMailWithMultipleFile(item, $"Reporte diario día {DateTime.Now:dd/MM/yyyy}", archivos, string.Empty,
                     "Hola Super Administrador", $"Se anexa el reporte del día {DateTime.Now:dd/MM/yyyy}, en Pdf y Excel", string.Empty,
                     @"Has recibido este e-mail porque eres usuario registrado en Lestoma-APP.<br>
                      <strong>Nota:</strong> Este correo se genera automáticamente, por favor no lo responda.");
                _logger.LogInformation($"Enviado el pdf  y excel a {item}");
                response.Mensaje = $"Enviado el pdf  y excel a {item}";
            }
            return response;
        }

        public async Task SendReportByFilter(string email)
        {
            _logger.LogInformation($"obteniendo el reporte");
            if (_cache.TryGetValue(Constants.CACHE_REPORTE_KEY, out ArchivoDTO archivo))
            {
                await _mailHelper.SendMailWithOneFile(email, string.Empty, $"Reporte {DateTime.Now:dd/MM/yyyy}",
                    archivo, $"Se anexa el reporte generado.", string.Empty,
                   @"Has recibido este e-mail porque eres usuario registrado en Lestoma-APP.<br>
                      <strong>Nota:</strong> Este correo se genera automáticamente, por favor no lo responda.");
                _logger.LogInformation($"Enviado el reporte a {email}");
            }
            else
            {
                _logger.LogInformation($"No hay reportes para generar.");
            }
        }
    }
}
