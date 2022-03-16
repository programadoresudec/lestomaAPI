using System.Threading.Tasks;

namespace lestoma.CommonUtils.Interfaces
{
    public interface IMailHelper
    {
        public Task SendMail(string correoDestino, string mensaje, string MensajeBoton = "",
          string title = "Lestoma", string body = "", string botonRuta = "", string footer = "por favor comuniquese con el administrador.");
        public Task SendMailWithArchives(string correoDestino, string folder, string archivo, string mensaje, string MIME, string MensajeBoton = "",
          string title = "Lestoma", string body = "", string botonRuta = "", string footer = "por favor comuniquese con el administrador.");
    }
}
