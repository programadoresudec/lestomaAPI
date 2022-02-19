using lestoma.CommonUtils.Interfaces;
using lestoma.CommonUtils.MyException;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace lestoma.Api.Helpers
{
    public class MailHelper : IMailHelper
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        public MailHelper(IWebHostEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _configuration = configuration;
        }
        public async Task SendCorreo(string correoDestino, string mensaje, string MensajeBoton = "",
          string title = "Lestoma", string body = "", string botonRuta = "", string footer = "por favor comuniquese con el administrador.")
        {

            try
            {
                string from = _configuration["Mail:From"];
                string smtp = _configuration["Mail:Smtp"];
                string port = _configuration["Mail:Port"];
                string password = _configuration["Mail:Password"];
                string folder = Path.Combine(_env.WebRootPath, "Recursos\\SendMail");
                string ruta = Path.Combine(folder, "PlantillaCorreo.html");
                var Emailtemplate = new StreamReader(ruta);
                var strBody = string.Format(Emailtemplate.ReadToEnd());
                Emailtemplate.Close();
                Emailtemplate.Dispose();
                Emailtemplate = null;
                var mailMessage = new MimeMessage();
                mailMessage.From.Add(new MailboxAddress("Lestoma", "tudec2020@gmail.com"));

                mailMessage.To.Add(new MailboxAddress(String.Empty, correoDestino));

                mailMessage.Subject = mensaje;
                strBody = strBody.Replace("TITLE", title);
                strBody = strBody.Replace("BODY", body);
                strBody = strBody.Replace("RUTA", botonRuta);
                strBody = strBody.Replace("TOKEN_O_LINK", MensajeBoton);
                strBody = strBody.Replace("FOOTER", footer);
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

        public async Task SendCorreoWithArchives(string correoDestino, string folder, string archivo, string mensaje, string MIME, string MensajeBoton = "",
          string title = "Lestoma", string body = "", string botonRuta = "", string footer = "por favor comuniquese con el administrador.")
        {
            try
            {
                string from = _configuration["Mail:From"];
                string smtp = _configuration["Mail:Smtp"];
                string port = _configuration["Mail:Port"];
                string password = _configuration["Mail:Password"];

                string plantilla = Path.Combine(_env.WebRootPath, "Recursos\\SendMail");
                string ruta = Path.Combine(plantilla, "PlantillaCorreo.html");
                var Emailtemplate = new StreamReader(ruta);
                var strBody = string.Format(Emailtemplate.ReadToEnd());
                Emailtemplate.Close();
                Emailtemplate.Dispose();
                Emailtemplate = null;
                var mailMessage = new MimeMessage();
                mailMessage.From.Add(new MailboxAddress("Lestoma", "tudec2020@gmail.com"));

                mailMessage.To.Add(new MailboxAddress(String.Empty, correoDestino));

                mailMessage.Subject = mensaje;
                strBody = strBody.Replace("TITLE", title);
                strBody = strBody.Replace("BODY", body);
                strBody = strBody.Replace("RUTA", botonRuta);
                strBody = strBody.Replace("TOKEN_O_LINK", MensajeBoton);
                strBody = strBody.Replace("FOOTER", footer);
                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = strBody
                };

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
                var buffer = File.ReadAllBytes(carpeta);
                bodyBuilder.Attachments.Add(Path.GetFileName(carpeta), buffer, new ContentType(MIME, Path.GetExtension(archivo)));

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
    }
}
