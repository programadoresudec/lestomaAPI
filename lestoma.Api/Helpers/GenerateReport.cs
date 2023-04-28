using ClosedXML.Excel;
using DinkToPdf;
using DinkToPdf.Contracts;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Interfaces;
using lestoma.CommonUtils.MyException;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace lestoma.Api.Helpers
{
    public class GenerateReport : IGenerateReport
    {
        private const string LOGO_CUNDINAMARCA = "https://i.postimg.cc/jdZ9LMyg/Escudo-Universidad-de-Cundinamarca.png";
        private const string LOGO_APP = "https://i.postimg.cc/ncBW1H8W/Logolestoma.png";

        private readonly IConverter _converter;
        private readonly IWebHostEnvironment _env;
        #region VISTAS HTML PDF
        private static readonly string VISTA_HTML_SUPERADMIN = @$"<html>
            <head <br>
              <br>
              <div align='left'> <img src='{LOGO_CUNDINAMARCA}' width='150'
                  height='200' <div align='right'> <img src='{LOGO_APP}' width='400'
                  height='150'> </div>
              <br>
              <div align='center'> <br>
                <h1>Universidad de Cundinamarca</h1>
                <h2>Aplicativo Móvil</h2>
                <h5>Super Administrador</h5>
              </div>
            </head>
               <br>
               <body>
                 <div><strong>Generado el dia:</strong> @ReporteFecha por la persona @NombreUsuario</div>
                 <br>
                 <div><strong>Filtrado por las fechas</strong></div>
                 <div> @FechaInicial- @FechaFinal</div>
                 <br>
                 <hr>
                 <table align='center'>
                   <tr>
                     <th align='center'>Nombre Upa</th>
                     <th align='center'>Generado por</th>
                     <th align='center'>Fecha de Servidor</th>
                     <th align='center'>Fecha de Dispositivo</th>
                     <th align='center'>Modulo</th>
                     <th align='center'>Componente</th>
                     <th align='center'>Estado inicial</th>
                     <th align='center'>Estado final</th>
                     <th align='center'>Tipo de función</th>
                   </tr>";

        private static readonly string VISTA_HTML_ADMIN = @$"<html>
            <head <br>
              <br>
              <div align='left'> <img src='{LOGO_CUNDINAMARCA}' width='150'
                  height='200' <div align='right'> <img src='{LOGO_APP}' width='400'
                  height='150'> </div>
              <br>
              <div align='center'> <br>
                <h1>Universidad de Cundinamarca</h1>
                <h2>Aplicativo Móvil</h2>
                <h5>Administrador</h5>
              </div>
            </head>
               <br>
               <body>
                   <div><strong>Generado el dia:</strong> @ReporteFecha por la persona @NombreUsuario</div>
                 <br>       
                 <div><strong>Upa:</strong> @NombreUpa</div>
                 <br>
                 <div><strong>Filtrado por las fechas</strong></div>
                 <div> @FechaInicial- @FechaFinal</div>
                 <br>
                 <hr>
                 <table align='center'>
                   <tr>
                     <th align='center'>Generado por</th>
                     <th align='center'>Fecha del Servidor</th>
                     <th align='center'>Fecha del Dispositivo</th>      
                     <th align='center'>Modulo</th>
                     <th align='center'>Componente</th>
                     <th align='center'>Estado inicial</th>
                     <th align='center'>Estado final</th>
                     <th align='center'>Tipo de función</th>
                   </tr>";

        #endregion

        public GenerateReport(IConverter converter, IWebHostEnvironment env)
        {
            _converter = converter;
            _env = env;
        }

        #region Generar reporte en excel
        public byte[] GenerateExcel(ReporteDTO reporte, bool IsSuperAdmin)
        {

            using (var workbook = new XLWorkbook())
            {
                try
                {
                    var worksheet = workbook.Worksheets.Add("Reporte");
                    var currentRow = 1;
                    // Cabeceros
                    worksheet.Cell(currentRow, 1).Value = "Generado por";
                    worksheet.Cell(currentRow, 2).Value = "Upa";
                    worksheet.Cell(currentRow, 3).Value = "Fecha del Servidor";
                    worksheet.Cell(currentRow, 4).Value = "Fecha del Dispositivo";
                    worksheet.Cell(currentRow, 5).Value = "Modulo";
                    worksheet.Cell(currentRow, 6).Value = "Componente";
                    worksheet.Cell(currentRow, 7).Value = "Estado inicial";
                    worksheet.Cell(currentRow, 8).Value = "Estado final";
                    worksheet.Cell(currentRow, 9).Value = "Tipo de función";
                    // Data
                    foreach (var item in reporte.Reporte)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = item.Usuario;
                        worksheet.Cell(currentRow, 2).Value = item.NombreUpa;
                        worksheet.Cell(currentRow, 3).Value = item.FechaServidor;
                        worksheet.Cell(currentRow, 4).Value = item.FechaDispositivo;
                        worksheet.Cell(currentRow, 5).Value = item.Modulo;
                        worksheet.Cell(currentRow, 6).Value = item.Componente;
                        worksheet.Cell(currentRow, 7).Value = item.SetPointIn;

                        double setPointOut;
                        if (double.TryParse(item.SetPointOut, out double valor))
                        {
                            setPointOut = valor;
                            worksheet.Cell(currentRow, 8).Value = setPointOut;
                        }
                        else
                        {
                            worksheet.Cell(currentRow, 8).Value = item.SetPointOut;
                        }
                        worksheet.Cell(currentRow, 9).Value = item.Estado;
                    }
                    worksheet.ColumnsUsed().AdjustToContents();
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return content;
                    }
                }
                catch (Exception ex)
                {
                    throw new HttpStatusCodeException(HttpStatusCode.InternalServerError,
                        $"Error: No se pudo generar el reporte en Excel, {ex.Message}");
                }
            }
        }

        #endregion

        #region Generar reporte en PDF
        public byte[] GeneratePdf(ReporteDTO reporte, bool IsSuperAdmin)
        {
            try
            {
                var html = GetHTMLString(reporte, IsSuperAdmin);
                var pathEstilo = Path.Combine(_env.WebRootPath, "Assets\\StylePdf.css");
                GlobalSettings globalSettings = new GlobalSettings();
                globalSettings.ColorMode = ColorMode.Color;
                globalSettings.Orientation = Orientation.Portrait;
                globalSettings.PaperSize = PaperKind.A4;
                globalSettings.Margins = new MarginSettings { Top = 25, Bottom = 25 };
                ObjectSettings objectSettings = new ObjectSettings();
                objectSettings.PagesCount = true;
                objectSettings.HtmlContent = html;
                WebSettings webSettings = new WebSettings()

                { DefaultEncoding = "utf-8", UserStyleSheet = pathEstilo };

                webSettings.DefaultEncoding = "utf-8";
                HeaderSettings headerSettings = new HeaderSettings();
                headerSettings.FontSize = 15;
                headerSettings.FontName = "Ariel";
                headerSettings.Right = "Página [page] De [toPage]";
                headerSettings.Line = true;
                FooterSettings footerSettings = new FooterSettings();
                footerSettings.FontSize = 12;
                footerSettings.FontName = "Ariel";
                footerSettings.Center = "Proyecto lestoma APP";
                footerSettings.Line = true;
                objectSettings.HeaderSettings = headerSettings;
                objectSettings.FooterSettings = footerSettings;
                objectSettings.WebSettings = webSettings;
                HtmlToPdfDocument htmlToPdfDocument = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings },
                };
                return _converter.Convert(htmlToPdfDocument);
            }
            catch (Exception ex)
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, $"Error: No se pudo generar el reporte en pdf, {ex.Message}");
            }

        }
        #endregion

        #region Agregar dinamicamente al html el reporte en pdf
        public static string GetHTMLString(ReporteDTO reporte, bool IsSuperAdmin)
        {
            var sb = new StringBuilder();
            if (IsSuperAdmin)
            {
                sb.Append(VISTA_HTML_SUPERADMIN);
            }
            else
            {
                sb.Append(VISTA_HTML_ADMIN);
            }
            sb.Replace("@ReporteFecha", DateTime.Now.ToString());
            sb.Replace("@NombreUsuario", reporte.UserGenerator);
            sb.Replace("@FechaInicial", reporte.FiltroFecha.FechaInicial.ToString());
            sb.Replace("@FechaFinal", reporte.FiltroFecha.FechaFinal.ToString());

            if (!IsSuperAdmin)
            {
                sb.Replace("@NombreUpa", reporte.Reporte[0].NombreUpa);
                foreach (var rep in reporte.Reporte)
                {
                    sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                    <td>{4}</td>
                                    <td>{5}</td>
                                    <td>{6}</td>
                                    <td>{7}</td>
                                  </tr>", rep.Usuario.ToLower(), rep.FechaServidor, rep.FechaDispositivo, rep.Modulo.ToLower(),
                        rep.Componente.ToLower(), rep.SetPointIn, rep.SetPointOut, rep.Estado.ToLower());
                }
            }
            else
            {
                foreach (var rep in reporte.Reporte)
                {
                    sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                    <td>{4}</td>
                                    <td>{5}</td>
                                    <td>{6}</td>
                                    <td>{7}</td>
                                    <td>{8}</td>
                                  </tr>", rep.NombreUpa, rep.Usuario, rep.FechaServidor, rep.FechaDispositivo, rep.Modulo.ToLower(),
                        rep.Componente.ToLower(), rep.SetPointIn, rep.SetPointOut, rep.Estado.ToLower());
                }
            }
            sb.Append(@"        </table>
                            </body>
                        </html>");
            return sb.ToString();
        }
        #endregion

        #region Generar reporte por formato
        public byte[] GenerateReportByFormat(GrupoTipoArchivo TipoFormato, ReporteDTO reporte, bool IsSuperAdmin)
        {
            byte[] reporteArchivo = null;
            if (TipoFormato == GrupoTipoArchivo.PDF)
            {
                reporteArchivo = GeneratePdf(reporte, IsSuperAdmin);
            }
            else if (TipoFormato == GrupoTipoArchivo.EXCEL)
            {
                reporteArchivo = GenerateExcel(reporte, IsSuperAdmin);
            }
            return reporteArchivo;
        }
        #endregion
    }
}
