using System;
using System.Collections.Generic;
using System.Text;

namespace lestoma.CommonUtils.Interfaces
{
    public interface IAuditoriaHelper
    {
        string GetDesencrytedIp();
        string GetUsuarioActual();
        string GetTipoDeAplicacion();
    }
}
