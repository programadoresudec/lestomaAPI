using lestoma.CommonUtils.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lestoma.CommonUtils.Interfaces
{
    public interface IMailHelper
    {
        public Task SendMail(string correoDestino, string mensaje, string MensajeBoton = "",
          string title = "Lestoma", string body = "", string botonRuta = "", string footer = "por favor comuniquese con el administrador.");
        public Task SendMailWithOneArchive(string correoDestino, string folder, string archivo, string mensaje, string MIME,
                byte[] ArchivoBytes, string MensajeBoton = "", string title = "Lestoma", string body = "",
                string botonRuta = "", string footer = "por favor comuniquese con el administrador.");
        public Task SendMailWithAllArchives(string correoDestino, string mensaje, List<ArchivoDTO> archivos,
            string MensajeBoton = "", string title = "Lestoma", string body = "",
            string botonRuta = "", string footer = "por favor comuniquese con el administrador.");
    }
}
