using lestoma.CommonUtils.Entities;
using lestoma.CommonUtils.Requests;
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

            var lista = (await (from persona in db.TablaUsuarios
                                join roles in db.TablaRoles on persona.RolId equals roles.Id
                                join estados in db.TablaEstadosUsuarios on persona.EstadoId equals estados.Id
                                where login.Clave.Equals(persona.Clave) && login.Email.Equals(persona.Email)

                                select new
                                {
                                    persona,
                                    roles,
                                    estados
                                }).ToListAsync());

            return lista.Select(m => new EUsuario

            {
                Apellido = m.persona.Apellido,
                Nombre = m.persona.Nombre,
                Id = m.persona.Id,
                Email = m.persona.Email,
                EstadoUsuario = { Id = m.estados.Id, DescripcionEstado = m.estados.DescripcionEstado },
                Rol = { Id = m.roles.Id, NombreRol = m.roles.NombreRol }
            }).FirstOrDefault();
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
