using lestoma.CommonUtils.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace lestoma.CommonUtils.DTOs
{
    public class DetalleUpaActividadDTO : AuditoriaDTO
    {
        public Guid UpaId { get; set; }
        public int UsuarioId { get; set; }
        public InfoUser User { get; set; } = new InfoUser();
        public InfoUpa Upa { get; set; } = new InfoUpa();
    }
    public class InfoUser
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string FullName => $"{Nombre} {Apellido}";
    }
    public class InfoUpa
    {
        public string Nombre { get; set; }
        public short CantidadActividades { get; set; }
    }
}
