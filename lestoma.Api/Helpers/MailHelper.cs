using lestoma.CommonUtils.Core;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Interfaces;
using lestoma.CommonUtils.MyException;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace lestoma.Api.Helpers
{
    public class MailHelper : IMailHelper
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        private readonly string _from;
        private readonly string _emailFrom;
        private readonly string _smtp;
        private readonly string _port;
        private readonly string _password;
        private readonly string folderPlantilla;
        public MailHelper(IWebHostEnvironment env, IConfiguration configuration, ILoggerManager logger)
        {
            _env = env;
            _logger = logger;
            _configuration = configuration;
            _from = Encryption.EncryptDecrypt.Decrypt(_configuration["Mail:From"]);
            _emailFrom = Encryption.EncryptDecrypt.Decrypt(_configuration["Mail:EmailFrom"]);
            _smtp = Encryption.EncryptDecrypt.Decrypt(_configuration["Mail:Smtp"]);
            _port = _configuration["Mail:Port"];
            _password = Encryption.EncryptDecrypt.Decrypt(_configuration["Mail:Password"]);
            folderPlantilla = Path.Combine(_env.WebRootPath, @"Recursos\SendMail");
        }
        #region Enviar correos
        public async Task SendMail(string correoDestino, string mensaje, string MensajeBoton = "",
         string title = "Lestoma-APP", string body = "", string botonRuta = "", string footer = "por favor comuniquese con el administrador.",
         bool successfulTransaction = false)
        {

            try
            {
                string ruta = Path.Combine(folderPlantilla, "PlantillaCorreo.html");
                var Emailtemplate = new StreamReader(ruta);
                var strBody = string.Format(Emailtemplate.ReadToEnd());
                Emailtemplate.Close();
                Emailtemplate.Dispose();
                Emailtemplate = null;
                var mailMessage = new MimeMessage();
                mailMessage.From.Add(new MailboxAddress("Lestoma-APP", _emailFrom));

                mailMessage.To.Add(new MailboxAddress(String.Empty, correoDestino));

                mailMessage.Subject = mensaje;
                strBody = strBody.Replace("@TITLE", title);
                strBody = strBody.Replace("@BODY", body);
                strBody = strBody.Replace("@RUTA", botonRuta);
                strBody = strBody.Replace("@TOKEN_O_LINK", MensajeBoton);
                strBody = strBody.Replace("@FOOTER", footer);
                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = strBody
                };
                mailMessage.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_smtp, int.Parse(_port));
                    await client.AuthenticateAsync(_from, _password);
                    await client.SendAsync(mailMessage);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                if (!successfulTransaction)
                {
                    throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, $"Error: No se pudo enviar el correo, {ex.Message}");
                }
                _logger.LogError(ex.Message, ex);
            }
        }

        #endregion

        #region Enviar correos con un archivo
        public async Task SendMailWithOneFile(string correoDestino, string folder, string mensaje, ArchivoDTO archivo,
            string MensajeBoton = "", string title = "Lestoma-APP", string body = "", string botonRuta = "",
            string footer = "por favor comuniquese con el administrador.", bool successfulTransaction = false)
        {
            try
            {
                string ruta = Path.Combine(folderPlantilla, "PlantillaCorreo.html");
                var Emailtemplate = new StreamReader(ruta);
                var strBody = string.Format(Emailtemplate.ReadToEnd());
                Emailtemplate.Close();
                Emailtemplate.Dispose();
                var mailMessage = new MimeMessage();
                mailMessage.From.Add(new MailboxAddress("Lestoma-APP", _emailFrom));
                mailMessage.To.Add(new MailboxAddress(String.Empty, correoDestino));
                mailMessage.Subject = mensaje;
                strBody = strBody.Replace("@TITLE", title);
                strBody = strBody.Replace("@BODY", body);
                strBody = strBody.Replace("@RUTA", botonRuta);
                strBody = strBody.Replace("@TOKEN_O_LINK", MensajeBoton);
                strBody = strBody.Replace("@FOOTER", footer);
                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = strBody
                };
                if (archivo.ArchivoBytes == null)
                {
                    string path = string.Empty;
                    if (string.IsNullOrWhiteSpace(folder))
                    {
                        path = $"{archivo.FileName}";
                    }
                    else
                    {
                        path = $"{folder}\\{archivo}";
                    }
                    string carpeta = Path.Combine(_env.WebRootPath, $"Recursos\\{path}");
                    archivo.ArchivoBytes = File.ReadAllBytes(carpeta);
                }

                var mime = Path.GetExtension(archivo.FileName).Contains("pdf") ? MimeKit.ContentType.Parse(MediaTypeNames.Application.Pdf)
                                                                               : MimeKit.ContentType.Parse(MediaTypeNames.Application.Octet);

                bodyBuilder.Attachments.Add(fileName: Path.GetFileName(archivo.FileName), data: archivo.ArchivoBytes, mime);

                mailMessage.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_smtp, int.Parse(_port));
                    await client.AuthenticateAsync(_from, _password);
                    await client.SendAsync(mailMessage);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                if (!successfulTransaction)
                {
                    throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, $"Error: No se pudo enviar el correo, {ex.Message}");
                }
                _logger.LogError(ex.Message, ex);
            }
        }
        #endregion

        #region Enviar correos con varios archivos
        public async Task SendMailWithMultipleFile(string correoDestino, string mensaje, List<ArchivoDTO> archivos
            , string MensajeBoton = "", string title = "Lestoma-APP",
            string body = "", string botonRuta = "", string footer = "por favor comuniquese con el administrador.", bool successfulTransaction = false)
        {
            try
            {
                string ruta = Path.Combine(folderPlantilla, "PlantillaCorreo.html");
                var Emailtemplate = new StreamReader(ruta);
                var strBody = string.Format(Emailtemplate.ReadToEnd());
                Emailtemplate.Close();
                Emailtemplate.Dispose();
                var mailMessage = new MimeMessage();
                mailMessage.From.Add(new MailboxAddress("Lestoma-APP", _emailFrom));
                mailMessage.To.Add(new MailboxAddress(String.Empty, correoDestino));
                mailMessage.Subject = mensaje;
                strBody = strBody.Replace("@TITLE", title);
                strBody = strBody.Replace("@BODY", body);
                strBody = strBody.Replace("@RUTA", botonRuta);
                strBody = strBody.Replace("@TOKEN_O_LINK", MensajeBoton);
                strBody = strBody.Replace("@FOOTER", footer);
                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = strBody
                };
                if (archivos.Count > 0)
                {
                    foreach (var item in archivos)
                    {
                        var mime = Path.GetExtension(item.FileName).Contains("pdf") ? MimeKit.ContentType.Parse(MediaTypeNames.Application.Pdf)
                                                                                    : MimeKit.ContentType.Parse(MediaTypeNames.Application.Octet);
                        bodyBuilder.Attachments.Add(Path.GetFileName(item.FileName), item.ArchivoBytes, mime);
                    }
                }
                mailMessage.Body = bodyBuilder.ToMessageBody();
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_smtp, int.Parse(_port));
                    await client.AuthenticateAsync(_from, _password);
                    await client.SendAsync(mailMessage);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                if (!successfulTransaction)
                {
                    throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, $"Error: No se pudo enviar el correo, {ex.Message}");
                }
                _logger.LogError(ex.Message, ex);
            }

        }
        #endregion
    }
}
