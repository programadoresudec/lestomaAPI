using System;
using System.Collections.Generic;
using System.Text;

namespace lestoma.CommonUtils.Interfaces
{
    public interface ICamposAuditoriaHelper
    {
        string ObtenerIp();
        string ObtenerUsuarioActual();
        string ObtenerTipoDeAplicacion();
    }
}
