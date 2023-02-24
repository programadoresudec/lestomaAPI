using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{

    [Table("componente_laboratorio", Schema = "laboratorio_lestoma")]
    public partial class EComponenteLaboratorio : ECamposAuditoria
    {

        [Column("id")]
        public Guid Id { get; set; }
        [Column("modulo_componente_id")]
        [Required(ErrorMessage = "Campo requerido.")]
        public Guid ModuloComponenteId { get; set; }
        [Column("actividad_id")]
        [Required(ErrorMessage = "Campo requerido.")]
        public Guid ActividadId { get; set; }
        [Column("upa_id")]
        [Required(ErrorMessage = "Campo requerido.")]
        public Guid UpaId { get; set; }
        [Column("nombre_componente")]
        [Required(ErrorMessage = "Campo requerido.")]
        public string NombreComponente { get; set; }
        [Column("descripcion_estado", TypeName = "jsonb")]
        [Required(ErrorMessage = "Campo requerido.")]
        public string JsonEstadoComponente { get; set; }
        [Column("direccion_registro", TypeName = "smallint")]
        [Required(ErrorMessage = "Campo requerido.")]
        public byte DireccionRegistro { get; set; }
        public EUpa Upa { get; set; }
        public EActividad Actividad { get; set; }
        public EModuloComponente ModuloComponente { get; set; }
    }
}
