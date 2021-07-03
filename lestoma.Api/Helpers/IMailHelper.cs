using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Api.Helpers
{
    public interface IMailHelper
    {
        public Task SendCorreo(string correoDestino, string codigo, string mensaje);
    }
}
