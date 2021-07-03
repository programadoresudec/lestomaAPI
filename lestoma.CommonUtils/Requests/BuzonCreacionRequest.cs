using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace lestoma.CommonUtils.Requests
{
    public class BuzonCreacionRequest
    {
        public int UsuarioId { get; set; }
        public DetalleBuzon Detalle { get; set; } = new DetalleBuzon();
        public byte[] Imagen { get; set; }
        public string Extension { get; set; }
    }

    public class DetalleBuzon
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string TipoDeGravedad { get; set; }
        public string PathImagen { get; set; }
    }
}
