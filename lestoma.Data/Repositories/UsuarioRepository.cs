using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.MyException;
using lestoma.CommonUtils.Requests;
using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
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
        public async Task<EUsuario> Logeo(LoginRequest login) =>
            await _dbSet.Include(o => o.EstadoUsuario).Include(e => e.Rol).Where(x => x.Email.Equals(login.Email)).FirstOrDefaultAsync();

        public async Task<EUsuario> GetByEmail(string email) => await _dbSet.FirstOrDefaultAsync(x => x.Email.Equals(email));


        public async Task<bool> ExisteCodigoVerificacion(string codigoRecuperacion) =>
            await _dbSet.AnyAsync(x => x.CodigoRecuperacion.Equals(codigoRecuperacion));

        public async Task<List<string>> GetCorreosRolSuperAdmin() => await _db.TablaUsuarios.Where(x => x.RolId == (int)TipoRol.SuperAdministrador
                    && x.EstadoId == (int)TipoEstadoUsuario.Activado).Select(x => x.Email).ToListAsync();

        public async Task<EUsuario> VerifyCodeByRecover(string codigo) =>
            await _dbSet.Where(x => x.CodigoRecuperacion.Equals(codigo))
            .FirstOrDefaultAsync();

        public EUsuario UsuarioByToken(string token) =>
            _db.TablaUsuarios.Include(o => o.Rol).SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));


        public short ExpiracionToken(int aplicacionId)
        {
            var expiracion = _db.TablaAplicaciones.FirstOrDefault(x => x.Id == aplicacionId);
            if (expiracion == null)
                return 15;
            return expiracion.TiempoExpiracionToken;
        }


        public async Task<IEnumerable<UserDTO>> GetUserswithoutUpa()
        {
            try
            {
                List<int> Ids = await (from detalle in _db.TablaUpasConActividades
                                       group detalle by new { detalle.UsuarioId } into grupo
                                       select grupo.Key.UsuarioId).ToListAsync();

                var parameters = new string[Ids.Count];
                var sqlParameters = new List<NpgsqlParameter>();
                for (var i = 0; i < Ids.Count; i++)
                {
                    parameters[i] = string.Format("@p{0}", i);
                    sqlParameters.Add(new NpgsqlParameter(parameters[i], Ids[i]));
                }

                var estadoId = new NpgsqlParameter("estadoId", (int)TipoEstadoUsuario.Activado);
                var rolSuperId = new NpgsqlParameter("rolSuperId", (int)TipoRol.SuperAdministrador);
                sqlParameters.Add(estadoId);
                sqlParameters.Add(rolSuperId);
                string consulta = $@"SELECT usuario.id, usuario.nombre, usuario.apellido, usuario.rol_id, rol.nombre_rol
                    FROM usuarios.usuario usuario INNER JOIN usuarios.rol rol on usuario.rol_id = rol.id
                    INNER JOIN usuarios.estado_usuario estado on estado.id = usuario.estado_id 
                    WHERE usuario.id NOT IN ({string.Join(", ", parameters)}) AND usuario.estado_id = @estadoId AND usuario.rol_id <> @rolSuperId";
                var users = _db.TablaUsuarios.FromSqlRaw(consulta, sqlParameters.ToArray()).OrderBy(x => x.Nombre);
                return await users.Select(x => new UserDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Apellido = x.Apellido,
                    RolId = x.RolId,
                    NombreRol = x.Rol.NombreRol
                }).ToListAsync();
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

        public async Task<IEnumerable<InfoUserDTO>> GetInfoUsers()
        {
            return await _db.TablaUsuarios
             .Select(x => new InfoUserDTO
             {
                 Id = x.Id,
                 Rol = new RolDTO
                 {
                     Id = x.Rol.Id,
                     NombreRol = x.Rol.NombreRol,
                 },
                 Nombre = x.Nombre,
                 Apellido = x.Apellido,
                 Email = x.Email,
                 Estado = new EstadoDTO
                 {
                     Id = x.EstadoUsuario.Id,
                     NombreEstado = x.EstadoUsuario.DescripcionEstado
                 },
                 Ip = x.Ip,
                 Session = x.Session,
                 TipoDeAplicacion = x.TipoDeAplicacion,
                 FechaCreacionServer = x.FechaCreacionServer,
                 FechaActualizacionServer = x.FechaActualizacionServer
             }).OrderBy(x => x.Nombre).ToListAsync();
        }

        public async Task<IEnumerable<EstadoDTO>> GetUserStatuses()
        {
            return await _db.TablaEstadosUsuarios
             .Select(x => new EstadoDTO
             {
                 Id = x.Id,
                 NombreEstado = x.DescripcionEstado
             }).OrderBy(y => y.NombreEstado).ToListAsync();
        }

        public async Task<IEnumerable<RolDTO>> GetRoles()
        {
            return await _db.TablaRoles.Where(x => x.Id != (int)TipoRol.SuperAdministrador)
            .Select(x => new RolDTO
            {
                Id = x.Id,
                NombreRol = x.NombreRol
            }).OrderBy(y => y.NombreRol).ToListAsync();
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            return await _dbSet.Select(x => new UserDTO
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Apellido = x.Apellido,
                RolId = x.RolId,
                NombreRol = x.Rol.NombreRol
            }).OrderBy(y => y.Nombre).ToListAsync();
        }
    }
}
