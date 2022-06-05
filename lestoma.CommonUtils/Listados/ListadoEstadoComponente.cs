﻿using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace lestoma.CommonUtils.Listados
{
    public class ListadoEstadoComponente
    {
        public List<EstadosComponentesDTO> Listado { get; set; }
        public ListadoEstadoComponente()
        {
            this.Listado = new Reutilizables().ReadJSON<List<EstadosComponentesDTO>>("EstadosDeComponentes.json");
        }
    }
}
