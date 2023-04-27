using lestoma.CommonUtils.Requests;
using System;

namespace lestoma.CommonUtils.DTOs
{
    public class BuzonDTO : AuditoriaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public NameDTO Upa { get; set; }
        public UserDTO User { get; set; } = new UserDTO();
    }
}
