
using lestoma.CommonUtils.Requests;
using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Data.DAO
{
    public class DAOUsuario
    {
        public async Task<EUsuario> Logeo(LoginRequest login, Mapeo db)
        {

            return await db.TablaUsuarios.Include(o => o.EstadoUsuario).
                Include(e => e.Rol).Where(x=>x.Email.Equals(login.Email)).FirstOrDefaultAsync();
        }

        public async Task<EUsuario> ExisteCorreo(string email, Mapeo db)
        {
            return await db.TablaUsuarios.Where(x => x.Email.Equals(email)).FirstOrDefaultAsync();
        }

        public async Task<bool> ExisteCodigoVerificacion(string codigoRecuperacion, Mapeo db)
        {
            return await db.TablaUsuarios.AnyAsync(x => x.CodigoRecuperacion.Equals(codigoRecuperacion));
        }

        public async Task<EUsuario> UsuarioByCodigoVerificacion(string codigo, Mapeo db)
        {
            return await db.TablaUsuarios.Where(x => x.CodigoRecuperacion.Equals(codigo)).FirstOrDefaultAsync();
        }
    }
}
