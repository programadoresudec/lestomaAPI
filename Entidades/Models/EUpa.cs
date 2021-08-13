using lestoma.CommonUtils.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace lestoma.Entidades.Models
{
    [Table("upa", Schema = "superadmin")]
    public class EUpa
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("nombre_upa")]
        public string Nombre { get; set; }
        [Column("superadmin_id")]
        public int SuperAdminId { get; set; } = (int)TipoRol.SuperAdministrador;
        [Column("cantidad_actividades")]
        public string CantidadActividades { get; set; }
        public List<EUpaActividad> Upas { get; set; }
    }
}
