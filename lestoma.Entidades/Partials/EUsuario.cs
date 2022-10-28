using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace lestoma.Entidades.Models
{
    public partial class EUsuario
    {
        [NotMapped]
        public string RefreshToken { get; set; }
        [JsonIgnore]
        public List<ETokensUsuarioByAplicacion> RefreshTokens { get; set; }
        [NotMapped]
        public int AplicacionId { get; set; }
        [NotMapped]
        public Guid UpaId { get; set; }
        [NotMapped]
        public string UserId { get; set; }
    }
}
