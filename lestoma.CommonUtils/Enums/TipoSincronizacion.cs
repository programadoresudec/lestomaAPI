using System.ComponentModel;

namespace lestoma.CommonUtils.Enums
{
    public enum TipoSincronizacion
    {
        [Description("Migrar datos online al dispositivo móvil.")]
        MigrateDataOnlineToDevice = 1,
        [Description("Migrar datos del dispositivo móvil al servidor de la nube.")]
        MigrateDataOfflineToServer = 2
    }
}
