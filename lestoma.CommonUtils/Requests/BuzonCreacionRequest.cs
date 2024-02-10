using lestoma.CommonUtils.DTOs;

namespace lestoma.CommonUtils.Requests
{
    public class BuzonCreacionRequest : AuditoriaDTO
    {
        public int UsuarioId { get; set; }
        public DetalleBuzonDTO Detalle { get; set; } = new DetalleBuzonDTO();
        public byte[] Imagen { get; set; }
        public string Extension { get; set; }
    }

    public class EditarEstadoBuzonRequest
    {
        public int BuzonId { get; set; }
        public EstadoBuzonDTO EstadoBuzon{ get; set; }
    }
    public class DetalleBuzonDTO
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string TipoDeGravedad { get; set; }
        public string PathImagen { get; set; }
        public EstadoBuzonDTO Estado { get; set; }
    }
    public class EstadoBuzonDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }   
}
