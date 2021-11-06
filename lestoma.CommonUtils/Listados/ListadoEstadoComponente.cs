using lestoma.CommonUtils.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace lestoma.CommonUtils.Listados
{
    public class ListadoEstadoComponente
    {
        public List<EstadosComponentes> Listado { get; set; }
        public ListadoEstadoComponente()
        {
            this.Listado = new Reutilizables().ReadJSON<List<EstadosComponentes>>("EstadosDeComponentes.json");
        }
    }
    public class EstadosComponentes
    {
        public string Id { get; set; }
        public string TipoEstado { get; set; }
        public string TercerByteTrama { get; set; }
        public string CuartoByteTrama { get; set; }
    }
}

