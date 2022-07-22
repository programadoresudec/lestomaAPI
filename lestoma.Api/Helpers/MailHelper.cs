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
using System.Threading.Tasks;

namespace lestoma.Api.Helpers
{
    public class MailHelper : IMailHelper
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly string from;
        private readonly string smtp;
        private readonly string port;
        private readonly string password;
        private readonly string folderPlantilla;
        public MailHelper(IWebHostEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _configuration = configuration;
            from = _configuration["Mail:From"];
            smtp = _configuration["Mail:Smtp"];
            port = _configuration["Mail:Port"];
            password = _configuration["Mail:Password"];
            folderPlantilla = Path.Combine(_env.WebRootPath, "Recursos\\SendMail");
        }
        #region Enviar correos
        public async Task SendMail(string correoDestino, string mensaje, string MensajeBoton = "",
         string title = "Lestoma", string body = "", string botonRuta = "", string footer = "por favor comuniquese con el administrador.")
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
                mailMessage.From.Add(new MailboxAddress("Lestoma", "tudec2020@gmail.com"));

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
                    await client.ConnectAsync(smtp, int.Parse(port), false);
                    await client.AuthenticateAsync(from, password);
                    await client.SendAsync(mailMessage);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, $"Error: No se pudo enviar el correo, {ex.Message}");
            }
        }

        #endregion

        #region Enviar correos con un archivo
        public async Task SendMailWithOneArchive(string correoDestino, string folder, string archivo, string mensaje, string MIME,
                byte[] ArchivoBytes, string MensajeBoton = "", string title = "Lestoma", string body = "",
                string botonRuta = "", string footer = "por favor comuniquese con el administrador.")
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
                mailMessage.From.Add(new MailboxAddress("Lestoma", "tudec2020@gmail.com"));
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
                if (ArchivoBytes == null)
                {
                    string path = string.Empty;
                    if (string.IsNullOrWhiteSpace(folder))
                    {
                        path = $"{archivo}";
                    }
                    else
                    {
                        path = $"{folder}\\{archivo}";
                    }
                    string carpeta = Path.Combine(_env.WebRootPath, $"Recursos\\{path}");
                    ArchivoBytes = File.ReadAllBytes(carpeta);
                }

                bodyBuilder.Attachments.Add(Path.GetFileName(archivo), ArchivoBytes, new ContentType(MIME, Path.GetExtension(archivo)));

                mailMessage.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(smtp, int.Parse(port), false);
                    await client.AuthenticateAsync(from, password);
                    await client.SendAsync(mailMessage);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, $"Error: No se pudo enviar el correo, {ex.Message}");
            }
        }
        #endregion

        #region Enviar correos con varios archivos
        public async Task SendMailWithAllArchives(string correoDestino, string mensaje, List<ArchivoDTO> archivos
            , string MensajeBoton = "", string title = "Lestoma",
            string body = "", string botonRuta = "", string footer = "por favor comuniquese con el administrador.")
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
                mailMessage.From.Add(new MailboxAddress("Lestoma", "tudec2020@gmail.com"));
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
                        bodyBuilder.Attachments.Add(Path.GetFileName(item.FileName), item.ArchivoBytes,
                            new ContentType(item.MIME, Path.GetExtension(item.FileName)));
                    }
                }
                mailMessage.Body = bodyBuilder.ToMessageBody();
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(smtp, int.Parse(port), false);
                    await client.AuthenticateAsync(from, password);
                    await client.SendAsync(mailMessage);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, $"Error: No se pudo enviar el correo, {ex.Message}");
            }

        }
        #endregion
    }
}
