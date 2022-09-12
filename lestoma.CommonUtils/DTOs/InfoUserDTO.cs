using System;
using System.Collections.Generic;
using System.Text;

namespace lestoma.CommonUtils.DTOs
{
    public class InfoUserDTO : AuditoriaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public RolDTO Rol { get; set; } = new RolDTO();
        public EstadoDTO Estado { get; set; } = new EstadoDTO();
        public string FullName => $"{Nombre} {Apellido}";
    }

    public class EstadoDTO
    {
        public int Id { get; set; }
        public string NombreEstado { get; set; }
    }

    public class RolDTO
    {
        public int Id { get; set; }
        public string NombreRol { get; set; }
    }
}
