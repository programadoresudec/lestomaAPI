using System.ComponentModel;

namespace lestoma.CommonUtils.Enums
{
    public enum TipoComunicacion
    {
        [Description("Punto a punto")]
        P2P = 73,
        [Description("Punto a Multipunto")]
        P2MP = 111,
    }
}
