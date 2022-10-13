using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace lestoma.CommonUtils.Requests
{
    public class RegistroRequest
    {
        [Required(ErrorMessage = "El estado id es requerido")]
        public int EstadoId { get; set; }
        [Required(ErrorMessage = "El rol id es requerido")]
        public int RolId { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido es requerido")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "El email es invalido")]
        public string Email { get; set; }
        [MaxLength(30, ErrorMessage = "La clave debe contener maximo 30 caracteres"), MinLength(8, ErrorMessage = "La clave debe contener minimo 8 caracteres")]
        [Required(ErrorMessage = "la clave es requerida")]
        public string Clave { get; set; }
    }

    public class RegistroUpdateRequest
    {
        [Required(ErrorMessage = "El id del usuario es requerido")]
        public int UsuarioId { get; set; }
        [Required(ErrorMessage = "El estado id es requerido")]
        public int EstadoId { get; set; }
        [Required(ErrorMessage = "El rol id es requerido")]
        public int RolId { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido es requerido")]
        public string Apellido { get; set; }
    }
}
