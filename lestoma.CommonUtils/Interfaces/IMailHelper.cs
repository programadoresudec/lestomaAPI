using lestoma.CommonUtils.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lestoma.CommonUtils.Interfaces
{
    public interface IMailHelper
    {
        public Task SendMail(string correoDestino, string mensaje, string MensajeBoton = "",
          string title = "Lestoma", string body = "", string botonRuta = "", string footer = "por favor comuniquese con el administrador.", 
          bool successfulTransaction = false);
        public Task SendMailWithOneFile(string correoDestino, string folder, string mensaje, ArchivoDTO archivo, 
            string MensajeBoton = "", string title = "Lestoma", string body = "",
                string botonRuta = "", string footer = "por favor comuniquese con el administrador.", bool successfulTransaction = false);
        public Task SendMailWithMultipleFile(string correoDestino, string mensaje, List<ArchivoDTO> archivos,
            string MensajeBoton = "", string title = "Lestoma", string body = "",
            string botonRuta = "", string footer = "por favor comuniquese con el administrador.", bool successfulTransaction = false);
    }
}
