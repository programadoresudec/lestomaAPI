﻿using lestoma.CommonUtils.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{
    [Table("upa_actividad", Schema = "superadmin")]
    public class EUpaActividad : ECamposAuditoria
    {
        [Column("upa_id")]
        public int UpaId { get; set; }
        [Column("actividad_id")]
        public int ActividadId { get; set; }
        [Column("usuario_id")]
        public int UsuarioId { get; set; }
        public EUpa Upa { get; set; }
        public EActividad Actividad { get; set; }
        public EUsuario Usuario { get; set; }
        [NotMapped]
        public List<ActividadRequest> Actividades { get; set; } = new List<ActividadRequest>();
    }
}
