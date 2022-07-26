﻿using lestoma.CommonUtils.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{
    [Table("buzon", Schema = "reportes")]
    public partial class EBuzon : ECamposAuditoria
    {

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("descripcion", TypeName = "jsonb")]
        public string Descripcion { get; set; }
        [Column("usuario_id")]
        public int UsuarioId { get; set; }
        [NotMapped]
        public UserDTO User { get; set; } = new UserDTO();
    }
}
