using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace lestoma.CommonUtils.Enums
{
    public enum TipoRol
    {
        [Description("Super Administrador")]
        SuperAdministrador = 1,
        [Description("Administrador")]
        Administrador = 2,
        [Description("Auxiliar")]
        Auxiliar = 3,
    }
}
