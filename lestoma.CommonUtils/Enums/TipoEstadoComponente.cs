using System.ComponentModel;

namespace lestoma.CommonUtils.Enums
{
    public enum TipoEstadoComponente
    {
        None = 0,
        [Description("AJUSTE")]
        Ajuste = 1,
        [Description("LECTURA")]
        Lectura = 2,
        [Description("ON-OFF")]
        OnOff = 3
    }
}
