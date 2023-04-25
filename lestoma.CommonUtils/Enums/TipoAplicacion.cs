using System.ComponentModel;

namespace lestoma.CommonUtils.Enums
{
    public enum TipoAplicacion
    {
        [Description("N/A")]
        ninguno = 0,
        [Description("App Movil")]
        AppMovil = 1,
        [Description("Web")]
        Web = 2
    }
}
