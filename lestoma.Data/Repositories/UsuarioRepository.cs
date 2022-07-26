using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.MyException;
using lestoma.CommonUtils.Requests;
using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace lestoma.Data.Repositories
{
    public class UsuarioRepository : GenericRepository<EUsuario>
    {
        private readonly LestomaContext _db;
        public UsuarioRepository(LestomaContext db)
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

        public List<UserDTO> GetUsersJustNames(bool isSuperAdmin)
        {
            try
            {
                int rolsuperId = (int)TipoRol.SuperAdministrador;
                int roladminId = (int)TipoRol.Administrador;
                List<int> Ids = new List<int>();
                if (isSuperAdmin)
                {
                    Ids.Add(rolsuperId);
                }
                else
                {
                    Ids.Add(rolsuperId);
                    Ids.Add(roladminId);
                }
                var parameters = new string[Ids.Count];
                var sqlParameters = new List<NpgsqlParameter>();
                for (var i = 0; i < Ids.Count; i++)
                {
                    parameters[i] = string.Format("@p{0}", i);
                    sqlParameters.Add(new NpgsqlParameter(parameters[i], Ids[i]));
                }

                var estadoId = new NpgsqlParameter("estadoId", (int)TipoEstadoUsuario.Activado);
                sqlParameters.Add(estadoId);
                string consulta = $@"SELECT usuario.id, usuario.nombre, usuario.apellido, usuario.rol_id, rol.nombre_rol
                    FROM usuarios.usuario usuario INNER JOIN usuarios.rol rol on usuario.rol_id = rol.id
                    INNER JOIN usuarios.estado_usuario estado on estado.id = usuario.estado_id 
                    WHERE usuario.rol_id NOT IN ({string.Join(", ", parameters)}) AND usuario.estado_id = @estadoId";
                var users = _db.TablaUsuarios.FromSqlRaw(consulta, sqlParameters.ToArray()).OrderBy(x => x.Nombre);
                var query = users.Select(x => new UserDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Apellido = x.Apellido,
                    RolId = x.RolId,
                    NombreRol = x.Rol.NombreRol
                }).ToList();

                return query;
            }
            catch (Exception ex)
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, @$"Error: {ex.Message}");
            }
        }



        public async Task<string> GetApplicationType(int tipoAplicacion)
        {
            var query = await _db.TablaAplicaciones.FindAsync(tipoAplicacion);

            return query == null ? "Local" : query.NombreAplicacion;
        }

        public async Task<IEnumerable<EUpaActividad>> GetActivitiesByUserId(int id)
        {
            return await _context.TablaUpasConActividades.Include(x => x.Actividad)
                .Where(x => x.UsuarioId == id).ToListAsync();
        }
    }
}
