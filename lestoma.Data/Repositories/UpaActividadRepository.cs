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
                    if (list.Count > 0)
                        _db.TablaUpasConActividades.RemoveRange(list);

                    foreach (var item in entidad.Actividades)
                    {
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
        public IQueryable<DetalleUpaActividadDTO> GetAllRelation()
        {

            //var queryef = _db.TablaUpasConActividades.GroupBy(x => new { upaId = x.UpaId, usuarioId = x.UsuarioId }).Select(x => new DetalleUpaActividadDTO
            //{
            //    UsuarioId = x.Key.usuarioId,
            //    UpaId = x.Key.upaId,
            //    Upa = new InfoUpa
            //    {
            //        CantidadActividades = (short)_db.TablaUpasConActividades.Where(y => y.UsuarioId == x.Key.usuarioId).Count(),
            //        Nombre = _db.TablaUpas.Where(y => y.Id == x.Key.upaId).Select(y => y.Nombre).FirstOrDefault(),
            //    },
            //    User = _db.TablaUsuarios.Where(y => y.Id == x.Key.usuarioId).Select(y => new InfoUser
            //    {
            //        Nombre = y.Nombre,
            //        Apellido = y.Apellido
            //    }).FirstOrDefault(),
            //});

            string consulta = $@"SELECT detalle.upa_id,
                                        detalle.usuario_id,
                                        upa.nombre_upa,
                                        us.nombre,
                                        us.apellido,
                                        max(detalle.fecha_creacion_server) as fecha_creacion_server,
                                        max(detalle.session)               as session,
                                        max(detalle.ip)                    as ip,
                                        max(detalle.tipo_de_aplicacion)    as tipo_de_aplicacion
                                 FROM superadmin.upa_actividad detalle
                                          INNER JOIN superadmin.upa upa on upa.id = detalle.upa_id
                                          INNER JOIN usuarios.usuario us on us.id = detalle.usuario_id
                                 group by detalle.upa_id, detalle.usuario_id, upa.nombre_upa, us.nombre, us.apellido";

            var listado = _db.TablaUpasConActividades.FromSqlRaw(consulta);
            //var listado = _db.TablaUpasConActividades.FromSqlInterpolated($@"SELECT detalle.upa_id,
            //                                                                        detalle.usuario_id,
            //                                                                        count(detalle.actividad_id)        as ActividadesAsignadas,
            //                                                                        upa.nombre_upa,
            //                                                                        us.nombre,
            //                                                                        us.apellido,
            //                                                                        max(detalle.fecha_creacion_server) as fecha_creacion_server,
            //                                                                        max(detalle.session)               as session,
            //                                                                        max(detalle.ip)                    as ip,
            //                                                                        max(detalle.tipo_de_aplicacion)    as tipo_de_aplicacion
            //                                                                 FROM superadmin.upa_actividad detalle
            //                                                                          INNER JOIN superadmin.upa upa on upa.id = detalle.upa_id
            //                                                                          INNER JOIN usuarios.usuario us on us.id = detalle.usuario_id
            //                                                                 group by detalle.upa_id, detalle.usuario_id, upa.nombre_upa, us.nombre, us.apellido");

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

