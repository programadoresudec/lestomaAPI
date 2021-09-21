using System;

namespace lestoma.Logica.Interfaces
{
    public interface IUpasActividadesService
    {


    }
    public class Auditoria
    {

        public Auditoria()
        {
            FechaGeneracion = DateTime.Now;
        }
        public string Tabla { get; set; }
        public DateTime FechaGeneracion { get; set; }


    }

}
