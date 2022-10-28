using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.MyException;
using lestoma.CommonUtils.Requests.Filters;
using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace lestoma.Data.Repositories
{
    public class UpaActividadRepository : GenericRepository<EUpaActividad>
    {
        private readonly LestomaContext _db;
        public UpaActividadRepository(LestomaContext db) : base(db)
        {
            _db = db;
        }

        public async Task CreateRelation(EUpaActividad entidad)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in entidad.Actividades)
                    {
                        entidad.ActividadId = item.Id;
                        _db.Add(entidad);
                    }
                    await _db.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ObtenerException(ex, entidad);
                }
            }
        }

        public async Task UpdateRelation(EUpaActividad entidad)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var list = await _db.TablaUpasConActividades.Where(x => x.UpaId == entidad.UpaId).ToListAsync();
                    if (list.Count > 0)
                        _db.TablaUpasConActividades.RemoveRange(list);

                    foreach (var item in entidad.Actividades)
                    {
                        entidad.ActividadId = item.Id;
                        _db.Add(entidad);
                    }
                    await _db.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ObtenerException(ex, entidad);
                }
            }
        }
        public IQueryable<DetalleUpaActividadDTO> GetAllRelation()
        {

            string consulta = $@"SELECT upa_id, usuario_id,u.nombre_upa, u.cantidad_actividades, us.nombre, 
                                us.apellido, max(ua.fecha_creacion_server) as fecha_creacion_server,
                                max(ua.session) as session,max(ua.ip) as ip,max(ua.tipo_de_aplicacion) as tipo_de_aplicacion 
                                FROM superadmin.upa_actividad ua
                                INNER JOIN superadmin.upa u on u.id = ua.upa_id
                                INNER JOIN usuarios.usuario us on us.id = ua.usuario_id
                                group by upa_id, usuario_id,u.nombre_upa, us.nombre, us.apellido, u.cantidad_actividades";

            var listado = _db.TablaUpasConActividades.FromSqlRaw(consulta);

            if (listado.Count() == 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay contenido.");
            }

            var query = listado.Select(x => new DetalleUpaActividadDTO
            {
                UsuarioId = x.UsuarioId,
                UpaId = x.UpaId,
                User = new InfoUser
                {
                    Nombre = x.Usuario.Nombre,
                    Apellido = x.Usuario.Apellido
                },
                Upa = new InfoUpa
                {
                    CantidadActividades = x.Upa.CantidadActividades,
                    Nombre = x.Upa.Nombre
                },
                TipoDeAplicacion = x.TipoDeAplicacion,
                FechaCreacionServer = x.FechaCreacionServer,
                Ip = x.Ip,
                Session = x.Session
            });
            return query;
        }



        public async Task<Guid> GetUpaByUserId(int id)
        {
            return await _db.TablaUpasConActividades.Include(x => x.Upa)
                     .Where(x => x.UsuarioId == id).Select(x => x.UpaId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<string>> GetActivities(int id, Guid UpaId)
        {
            return await _db.TablaUpasConActividades.Include(x => x.Actividad)
                .Where(x => x.UsuarioId == id && x.UpaId == UpaId).Select(x => x.Actividad.Nombre).ToListAsync();
        }

        public async Task<List<NameDTO>> GetActivitiesByUpaUserId(UpaUserFilterRequest filtro)
        {
            var query = await _db.TablaUpasConActividades.Include(x => x.Actividad).Where(x => x.UpaId == filtro.UpaId && x.UsuarioId == filtro.UsuarioId)
                .Select(x => new NameDTO
                {
                    Id = x.ActividadId,
                    Nombre = x.Actividad.Nombre
                }).ToListAsync();
            return query;
        }
    }
}

