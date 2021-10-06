﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{
    [Table("upa", Schema = "superadmin")]
    public class EUpa : ECamposAuditoria
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("nombre_upa")]
        public string Nombre { get; set; }
        [Column("superadmin_id")]
        public int SuperAdminId { get; set; }
        [Column("cantidad_actividades")]
        public short CantidadActividades { get; set; }
        public List<EUpaActividad> Upas { get; set; }
    }
}