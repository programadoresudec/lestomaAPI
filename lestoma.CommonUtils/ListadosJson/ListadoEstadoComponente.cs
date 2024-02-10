using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace lestoma.CommonUtils.ListadosJson
{
    public class ListadoEstadoComponente
    {
        public List<EstadoComponenteDTO> Listado { get; set; }
        public ListadoEstadoComponente()
        {
            Listado = new Reutilizables().ReadJSON<List<EstadoComponenteDTO>>("EstadosDeComponentes.json");
        }

        public List<EstadoComponenteDTO> EstadosCreate()
        {
            var tempGuid = Guid.Parse("C781773B-7D7C-47F7-B5D0-34A4943BA907");
            var dato = Listado.Where(x => x.Id == tempGuid).FirstOrDefault();
            if (dato != null)
            {
                Listado.Remove(dato);
            }
            return Listado;
        }

        public EstadoComponenteDTO GetEstadoAjuste()
        {
            var tempGuid = Guid.Parse("C781773B-7D7C-47F7-B5D0-34A4943BA907");
            return Listado.Where(x => x.Id == tempGuid).FirstOrDefault();
        }
    }
}

