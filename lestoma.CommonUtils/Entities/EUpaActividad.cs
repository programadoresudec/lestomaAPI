using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace lestoma.CommonUtils.Entities
{
    [Table("upa_actividad", Schema = "superadmin")]
    public class EUpaActividad
    {
        [Column("upa_id")]
        public int UpaId { get; set; }
        [Column("actividad_id")]
        public int ActividadId { get; set; }
        [Column("usuario_id")]
        public int UsuarioId { get; set; }
        public EUpa Upa { get; set; }
        public EActividad Actividad { get; set; }
        [Column("fecha_creacion")]
        public DateTime FechaCreacion { get; set; }
        [Column("descripcion")]
        public string Descripcion { get; set; }
        [Column("ip")]
        public string Ip { get; set; }
        [Column("session")]
        public string Session { get; set; }
        [Column("tipo_de_aplicacion")]
        public string TipoDeAplicacion { get; set; }
    }
}
