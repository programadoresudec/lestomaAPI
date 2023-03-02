using Hangfire;
using lestoma.CommonUtils.Constants;
using lestoma.CommonUtils.Core;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Interfaces;
using lestoma.CommonUtils.MyException;
using lestoma.CommonUtils.Requests.Filters;
using lestoma.Data.Repositories;
using lestoma.Logica.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
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

        #region Obtiene la data del reporte diario
        [AutomaticRetry(Attempts = 2)]
        public async Task<ResponseDTO> GetDailyReport()
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
        #endregion

        #region Obtener tiempo de informe diario
        public async Task<ResponseDTO> GetDailyReportTime()
        {
            var time = await _repositorio.GetDailyReportTime(Constants.KEY_REPORT_DAILY);
            if (time == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, @$"Error: No se pudo encontrar la hora del job.");

            return Responses.SetOkResponse(new TimeJobDTO { Time = time.Value });
        }
        #endregion

        #region Generar el documento PDF y Excel diariamente
        public async Task<ResponseDTO> GenerateDailyReport(ReporteDTO reporte)
        {
            List<ArchivoDTO> archivos = new List<ArchivoDTO>();
            ResponseDTO response = new ResponseDTO();
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
                response.MensajeHttp = $"Enviado el pdf  y excel a {item}";
            }
            return response;
        }
        #endregion

        #region Obtiene la data del reporte por fechas  upa y los componentes correspondientes
        public async Task<(ReporteDTO reporte, ArchivoDTO archivo)> GetReportByComponents(ReportComponentFilterRequest requestFilter, string email)
        {
            var (MIME, FILENAME) = GetFile(requestFilter.Filtro.TipoFormato);

            if (requestFilter.Filtro.UpaId != Guid.Empty)
                await ExistUpa(requestFilter.Filtro.UpaId);

            foreach (var item in requestFilter.ComponentesId)
            {
                bool existe = await _componenteRepository.AnyWithCondition(x => x.Id == item);
                if (!existe)
                {
                    throw new HttpStatusCodeException(HttpStatusCode.NotFound, $"Error: No se pudo encontrar el componente con el id: {item}");
                }
            }
            var listado = await _repositorio.ReportByComponents(requestFilter);
            if (listado.Reporte.Count == 0)
            {
                await _mailHelper.SendMail(email, "No hay datos para generar el reporte.", "No hay data.",
                    "Has recibido este e-mail porque eres usuario registrado en Lestoma-APP.",
                    $"No se genero ningun reporte con el filtro fecha inicial: {requestFilter.Filtro.FechaInicial} Fecha final: {requestFilter.Filtro.FechaFinal}", string.Empty,
                    "<strong>Nota:</strong> Este correo se genera automáticamente, por favor no lo responda.");
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, @"No hay datos para generar el reporte.");
            }
            ArchivoDTO archivo = new()
            {
                FileName = FILENAME,
                MIME = MIME
            };
            return (listado, archivo);
        }
        #endregion

        #region Obtiene la data del reporte por fechas y upa
        public async Task<(ReporteDTO reporte, ArchivoDTO archivo)> GetReportByDate(ReportFilterRequest filtro, string email)
        {
            var (MIME, FILENAME) = GetFile(filtro.TipoFormato);

            if (filtro.UpaId != Guid.Empty)
                await ExistUpa(filtro.UpaId);

            var listado = await _repositorio.ReportByDate(filtro);
            if (listado.Reporte.Count == 0)
            {
                await _mailHelper.SendMail(email, "No hay datos para generar el reporte.", "No hay data.",
               "Has recibido este e-mail porque eres usuario registrado en Lestoma-APP.",
               $"No se genero ningun reporte con el filtro fecha inicial: {filtro.FechaInicial} Fecha final: {filtro.FechaFinal}", string.Empty,
               "<strong>Nota:</strong> Este correo se genera automáticamente, por favor no lo responda.");
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, @"No hay datos para generar el reporte.");
            }
            ArchivoDTO archivo = new()
            {
                FileName = FILENAME,
                MIME = MIME
            };
            return (listado, archivo);
        }
        #endregion

        #region Genera el reporte por fecha inicial y fecha final dependiendo el formato dado
        [AutomaticRetry(Attempts = 2)]
        public async Task<ArchivoDTO> GenerateReportByDate(ReportFilterRequest filtro, bool isSuper, string email)
        {
            var (reporte, archivo) = await GetReportByDate(filtro, email);
            reporte.FiltroFecha = new DateFilterRequest()
            {
                FechaInicial = filtro.FechaInicial,
                FechaFinal = filtro.FechaFinal
            };
            archivo.ArchivoBytes = _generateReports.GenerateReportByFormat(filtro.TipoFormato, reporte, isSuper);
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal);
            _cache.Set(Constants.CACHE_REPORTE_KEY, archivo, cacheEntryOptions);
            return archivo;
        }
        #endregion

        #region Genera el reporte por componentes dependiendo el formato dado
        [AutomaticRetry(Attempts = 2)]
        public async Task<ArchivoDTO> GenerateReportByComponents(ReportComponentFilterRequest obj, bool isSuper, string email)
        {
            var (reporte, archivo) = await GetReportByComponents(obj, email);
            archivo.ArchivoBytes = _generateReports.GenerateReportByFormat(obj.Filtro.TipoFormato, reporte, isSuper);
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal);
            _cache.Set(Constants.CACHE_REPORTE_KEY, archivo, cacheEntryOptions);
            return archivo;
        }
        #endregion

        #region Envia el reporte dado el filtro correspondiente
        [AutomaticRetry(Attempts = 2)]
        public async Task SendReportByFilter(string email)
        {
            try
            {
                _logger.LogInformation($"obteniendo el reporte...");
                if (_cache.TryGetValue(Constants.CACHE_REPORTE_KEY, out ArchivoDTO archivo))
                {
                    await _mailHelper.SendMailWithOneFile(email, string.Empty, $"Reporte {DateTime.Now:dd/MM/yyyy}",
                        archivo, $"Se anexa el reporte generado.", string.Empty,
                        "Has recibido este e-mail porque eres usuario registrado en Lestoma-APP.", string.Empty,
                       "<strong>Nota:</strong> Este correo se genera automáticamente, por favor no lo responda.");
                    _logger.LogInformation($"Enviado el reporte a {email}");
                }
                else
                {
                    _logger.LogInformation($"No hay reportes para generar.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
        #endregion

        #region Verifica si existe la upa
        private async Task ExistUpa(Guid upaId)
        {
            if (upaId != Guid.Empty)
            {
                var existe = await _upaRepository.AnyWithCondition(x => x.Id == upaId);
                if (!existe)
                    throw new HttpStatusCodeException(HttpStatusCode.NotFound, @$"Error: No se pudo encontrar la upa indicada.");
            }
        } 
        #endregion

        #region Obtiene el grupo de archivo MIME y FileName
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
                    Mime = MediaTypeNames.Application.Octet;
                    FileName = $"Reporte_{dateString}.pdf";
                    break;
                case GrupoTipoArchivo.EXCEL:
                    Mime = MediaTypeNames.Application.Octet;
                    FileName = $"Reporte_{dateString}.xlsx";
                    break;
            }
            return (Mime, FileName);
        } 
        #endregion

    }
}
