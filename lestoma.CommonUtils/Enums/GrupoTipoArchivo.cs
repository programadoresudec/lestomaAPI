using System.ComponentModel;

namespace lestoma.CommonUtils.Enums
{
    public enum GrupoTipoArchivo
    {
        [Description("Imagen")]
        IMAGEN = 1,
        [Description("PDF")]
        PDF = 2,
        [Description("EXCEL")]
        EXCEL = 3,
        [Description("CSV")]
        CSV = 4
    }
}
