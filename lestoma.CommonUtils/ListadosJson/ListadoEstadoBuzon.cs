using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Requests;
using System.Collections.Generic;

namespace lestoma.CommonUtils.ListadosJson
{
    public class ListadoEstadoBuzon
    {
        public List<EstadoBuzonDTO> Listado { get; set; }
        public ListadoEstadoBuzon()
        {
            Listado = new Reutilizables().ReadJSON<List<EstadoBuzonDTO>>("EstadosBuzon.json");
        }
    }
}
