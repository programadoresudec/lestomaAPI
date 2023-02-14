using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace lestoma.CommonUtils.Enums
{
    public enum TipoSincronizacion
    {
        [Description("Migrar data online al dispositivo movil")]
        MigrateDataOnlineToDevice = 1,
        [Description("Migrar data offline al servidor")]
        MigrateDataOfflineToServer = 2
    }
}
