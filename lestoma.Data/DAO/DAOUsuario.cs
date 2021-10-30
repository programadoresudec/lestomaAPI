
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Requests;
using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Data.DAO
{
    public class DAOUsuario : GenericRepository<EUsuario>
    {
        private readonly Mapeo _db;
        public DAOUsuario(Mapeo db)
            : base(db)
        {
            _db = db;
        }
        public async Task<EUsuario> Logeo(LoginRequest login)
        {

            return await _db.TablaUsuarios.Include(o => o.EstadoUsuario).
                Include(e => e.Rol).Where(x => x.Email.Equals(login.Email)).FirstOrDefaultAsync();
        }

        public async Task<EUsuario> GetByEmail(string email)
        {
            return await _db.TablaUsuarios.FirstOrDefaultAsync(x => x.Email.Equals(email));
        }

        public async Task<bool> ExisteCodigoVerificacion(string codigoRecuperacion)
        {
            return await _db.TablaUsuarios.AnyAsync(x => x.CodigoRecuperacion.Equals(codigoRecuperacion));
        }

        public async Task<EUsuario> UsuarioByCodigoVerificacion(string codigo) =>
            await _db.TablaUsuarios.Where(x => x.CodigoRecuperacion.Equals(codigo))
            .FirstOrDefaultAsync();

        public EUsuario UsuarioByToken(string token)
        {
            return _db.TablaUsuarios.Include(o => o.Rol).SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
        }

        public short ExpiracionToken(int aplicacionId) =>
            _db.TablaAplicaciones.FirstOrDefault(x => x.Id == aplicacionId).TiempoExpiracionToken;

        public List<UserDTO> GetUsersJustNames()
        {
            try
            {
                var id = new NpgsqlParameter("id", (int)TipoRol.SuperAdministrador);
                string consulta = "SELECT uu.id, uu.nombre, uu.apellido FROM usuarios.usuario uu" +
                    $" INNER JOIN usuarios.rol ur on uu.rol_id = ur.id WHERE ur.id != @id";
                var users = _db.TablaUsuarios.FromSqlRaw(consulta, id).OrderBy(x => x.Nombre);
                var query = users.Select(x => new UserDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Apellido = x.Apellido
                }).ToList();

                return query;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<string> GetApplicationType(int tipoAplicacion)
        {
            var query = await _db.TablaAplicaciones.FindAsync(tipoAplicacion);

            return query == null ? "Local" : query.NombreAplicacion;
        }
    }
}
