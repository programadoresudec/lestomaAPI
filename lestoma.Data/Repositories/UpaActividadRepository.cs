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
                        var objeto = new EUpaActividad
                        {
                            ActividadId = item.Id,
                            UpaId = entidad.UpaId,
                            UsuarioId = entidad.UsuarioId,
                        };
                        _db.Add(objeto);
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
                    var list = await _db.TablaUpasConActividades.Where(x => x.UsuarioId == entidad.UsuarioId).ToListAsync();
                    var fechacreacion = list[0].FechaCreacionServer;
                    if (list.Count > 0)
                        _db.TablaUpasConActividades.RemoveRange(list);

                    foreach (var item in entidad.Actividades)
                    {
                        var objeto = new EUpaActividad
                        {
                            ActividadId = item.Id,
                            UpaId = entidad.UpaId,
                            UsuarioId = entidad.UsuarioId,
                            FechaCreacionServer = fechacreacion,
                            FechaActualizacionServer = DateTime.Now
                        };
                        _db.Add(objeto);
                    }

                    await _db.SaveChangesAsync(acceptAllChangesOnSuccess: false);
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
            string consulta = $@"SELECT detalle.upa_id,
                                        detalle.usuario_id,
                                        upa.nombre_upa,
                                        us.nombre,
                                        us.apellido,
                                        MAX(detalle.fecha_creacion_server)      as fecha_creacion_server,
                                        MAX(detalle.fecha_actualizacion_server) as fecha_actualizacion_server,
                                        MAX(detalle.session)                    as session,
                                        MAX(detalle.ip)                         as ip,
                                        MAX(detalle.tipo_de_aplicacion)         as tipo_de_aplicacion
                                 FROM superadmin.upa_actividad detalle
                                          INNER JOIN superadmin.upa upa on upa.id = detalle.upa_id
                                          INNER JOIN usuarios.usuario us on us.id = detalle.usuario_id
                                 GROUP BY detalle.upa_id, detalle.usuario_id, upa.nombre_upa, us.nombre, us.apellido
                                 ORDER BY upa.nombre_upa";

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
                    CantidadActividades = (short)_db.TablaUpasConActividades.Where(y => y.UsuarioId == x.UsuarioId).Count(),
                    Nombre = x.Upa.Nombre
                },
                TipoDeAplicacion = x.TipoDeAplicacion,
                FechaCreacionServer = x.FechaCreacionServer,
                FechaActualizacionServer = x.FechaActualizacionServer,
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

        public async Task<IEnumerable<NameDTO>> GetActivitiesByUpaUserId(UpaUserFilterRequest filtro)
        {
            var query = await _db.TablaUpasConActividades.Include(x => x.Actividad)
                .Where(x => x.UpaId == filtro.UpaId && x.UsuarioId == filtro.UsuarioId)
                .Select(x => new NameDTO
                {
                    Id = x.ActividadId,
                    Nombre = x.Actividad.Nombre
                }).ToListAsync();
            return query;
        }

        public async Task<IEnumerable<NameDTO>> GetActivitiesByUpaId(Guid idUpa)
        {
            return await (from upaActividad in _db.TablaUpasConActividades
                          join Actividad in _db.TablaActividades on upaActividad.ActividadId equals Actividad.Id
                          where upaActividad.UpaId == idUpa
                          group Actividad by new { Actividad.Id, Actividad.Nombre } into grupo
                          select new NameDTO
                          {
                              Id = grupo.Key.Id,
                              Nombre = grupo.Key.Nombre
                          }).ToListAsync();
        }
    }
}

