using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Helpers;
using System.Collections.Generic;

namespace lestoma.CommonUtils.Listados
{
    public class ListadoEstadoComponente
    {
        public List<EstadoComponenteDTO> Listado { get; set; }
        public ListadoEstadoComponente()
        {
            this.Listado = new Reutilizables().ReadJSON<List<EstadoComponenteDTO>>("EstadosDeComponentes.json");
        }
    }
}

