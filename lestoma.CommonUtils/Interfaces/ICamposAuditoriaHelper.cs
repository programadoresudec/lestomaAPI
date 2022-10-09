using System;
using System.Collections.Generic;
using System.Text;

namespace lestoma.CommonUtils.Interfaces
{
    public interface IAuditoriaHelper
    {
        string ObtenerIp();
        string ObtenerUsuarioActual();
        string ObtenerTipoDeAplicacion();
    }
}
