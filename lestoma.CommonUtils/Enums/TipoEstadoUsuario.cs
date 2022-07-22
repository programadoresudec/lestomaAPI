using System.ComponentModel;

namespace lestoma.CommonUtils.Enums
{
    public enum TipoEstadoUsuario
    {
        [Description("verificar cuenta")]
        CheckCuenta = 1,
        [Description("Activado")]
        Activado = 2,
        [Description("Inactivo")]
        Inactivo = 3,
        [Description("Bloqueado")]
        Bloqueado = 4,
    }
}
