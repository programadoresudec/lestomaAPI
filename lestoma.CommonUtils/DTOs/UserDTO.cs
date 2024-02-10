namespace lestoma.CommonUtils.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int RolId { get; set; }
        public string FullName => $"{Nombre} {Apellido}";
        public string NombreRol { get; set; }
    }
}
