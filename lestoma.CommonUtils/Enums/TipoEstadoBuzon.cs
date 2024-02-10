using System.ComponentModel;

namespace lestoma.CommonUtils.Enums
{
    public enum TipoEstadoBuzon
    {
        [Description("En Revisión")]
        Pendiente = 1,
        [Description("Escalado")]
        Escalado = 2,
        [Description("Completado")]
        Finalizado = 3,
    }
}
