using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Mail;
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

        public async Task SendCorreo(string correoDestino, string codigo, string mensaje)
        {

            try
            {
                string from = _configuration["Mail:From"];
                string smtp = _configuration["Mail:Smtp"];
                string port = _configuration["Mail:Port"];
                string password = _configuration["Mail:Password"];

                 string folder = Path.Combine(_env.WebRootPath,"Recursos\\SendMail");
                string ruta = Path.Combine(folder, "ChangePassword.html");

                var Emailtemplate = new StreamReader(ruta);
                var strBody = string.Format(Emailtemplate.ReadToEnd(), codigo);
                Emailtemplate.Close();
                Emailtemplate.Dispose();
                Emailtemplate = null;
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(smtp);
                //Especificamos el correo desde el que se enviará el Email y el nombre de la persona que lo envía
                mail.From = new MailAddress("vasborsas@gmail.com", "Lestoma");
                SmtpServer.Host = smtp;
                //Aquí ponemos el asunto del correo
                mail.Subject = mensaje;
                strBody = strBody.Replace("TOKENCODIGO", codigo);
                mail.Body = strBody;
                //mail.Body = "Por favor ingrese al siguiente link para recuperar su contraseña";
                //Especificamos a quien enviaremos el Email, no es necesario que sea Gmail, puede ser cualquier otro proveedor
                mail.To.Add(correoDestino);
                //Si queremos enviar archivos adjuntos tenemos que especificar la ruta en donde se encuentran
                //mail.Attachments.Add(new Attachment(@"C:\Documentos\carta.docx"));
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;
                //Configuracion del SMTP
                SmtpServer.Port = int.Parse(port); //Puerto que utiliza Gmail para sus servicios
                                                   //Especificamos las credenciales con las que enviaremos el mail
                SmtpServer.Credentials = new System.Net.NetworkCredential(from, password);
                SmtpServer.EnableSsl = true;
                await SmtpServer.SendMailAsync(mail);
                mail.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
