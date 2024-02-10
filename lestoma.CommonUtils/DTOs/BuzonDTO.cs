using lestoma.CommonUtils.Enums;

namespace lestoma.CommonUtils.DTOs
{
    public class BuzonDTO : AuditoriaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int EstadoId { get; set; }
        public NameDTO Upa { get; set; }
        public UserDTO User { get; set; } = new UserDTO();
        public EstadoColorBuzonDTO Estado => GetColor(this.EstadoId);
        public bool IsVisibleButtonStatus => EstadoId != (int)TipoEstadoBuzon.Finalizado;
        private EstadoColorBuzonDTO GetColor(int EstadoId)
        {
            EstadoColorBuzonDTO estado = new EstadoColorBuzonDTO();
            if (EstadoId != 0)
            {
                switch (EstadoId)
                {
                    case (int)TipoEstadoBuzon.Pendiente:
                        estado.Color = "#BB270D";
                        estado.Nombre = EnumConfig.GetDescription(TipoEstadoBuzon.Pendiente);
                        break;
                    case (int)TipoEstadoBuzon.Escalado:
                        estado.Color = "#EA5D0D";
                        estado.Nombre = EnumConfig.GetDescription(TipoEstadoBuzon.Escalado);
                        break;
                    case (int)TipoEstadoBuzon.Finalizado:
                        estado.Color = "#32720D";
                        estado.Nombre = EnumConfig.GetDescription(TipoEstadoBuzon.Finalizado);
                        break;

                }
            }
            return estado;
        }
    }
    public class EstadoColorBuzonDTO
    {
        public string Nombre { get; set; }
        public string Color { get; set; }
    }
}
