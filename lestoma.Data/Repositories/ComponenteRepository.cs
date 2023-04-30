using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.MyException;
using lestoma.CommonUtils.Requests.Filters;
using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace lestoma.Data.Repositories
{
    public class ComponenteRepository : GenericRepository<EComponenteLaboratorio>
    {
        private readonly LestomaContext _db;

        public ComponenteRepository(LestomaContext db) : base(db)
        {
            _db = db;
        }
        public async Task<bool> ExisteComponente(string nombre, Guid id, bool insertOrUpdate = false)
        {
            if (!insertOrUpdate)
            {
                return await _dbSet.AnyAsync(x => x.NombreComponente.ToLower().Equals(nombre.ToLower()));

            }
            else
            {
                return await _dbSet.AnyAsync(x => x.NombreComponente.ToLower().Equals(nombre.ToLower()) && x.Id != id);

            }
        }

        public async Task<ESuperAdministrador> GetSuperAdmin()
        {
            var user = await _db.TablaUsuarios.FirstOrDefaultAsync(x => x.RolId == (int)TipoRol.SuperAdministrador);
            if (user == null)
            {
                return null;
            }
            return await _db.TablaSuperAdministradores.FirstOrDefaultAsync(x => x.UsuarioId == user.Id);
        }

        public async Task<IEnumerable<NameDTO>> GetComponentesJustNames()
        {
            var query = await _dbSet.Select(x => new NameDTO
            {
                Id = x.Id,
                Nombre = x.NombreComponente,
            }).OrderBy(x => x.Nombre).ToListAsync();
            return query;
        }

        public IQueryable<ListadoComponenteDTO> GetAllFilter(UpaActivitiesFilterRequest upaActivitiesFilter)
        {
            var query = _dbSet.AsNoTracking();
            if (upaActivitiesFilter.UpaId != Guid.Empty)
            {
                query = query.Where(x => x.UpaId == upaActivitiesFilter.UpaId);
            }
            if (upaActivitiesFilter.ActividadesId != null && upaActivitiesFilter.ActividadesId.Any())
            {
                query = query.Where(x => upaActivitiesFilter.ActividadesId.Contains(x.ActividadId));
            }
            var listado = query.OrderBy(y => y.NombreComponente).Select(x => new ListadoComponenteDTO
            {
                Actividad = x.Actividad.Nombre,
                Nombre = x.NombreComponente,
                FechaCreacionServer = x.FechaCreacionServer,
                FechaActualizacionServer = x.FechaActualizacionServer,
                Modulo = x.ModuloComponente.Nombre,
                Ip = x.Ip,
                Session = x.Session,
                Id = x.Id,
                JsonEstadoComponente = x.JsonEstadoComponente,
                TipoDeAplicacion = x.TipoDeAplicacion,
                Upa = x.Upa.Nombre,
                DireccionRegistro = x.DireccionRegistro
            });
            return listado;
        }

        public async Task<InfoComponenteDTO> GetInfoById(Guid id)
        {
            return await _dbSet.Where(x => x.Id == id).Include(y => y.Actividad).Include(y => y.Upa).Include(y => y.ModuloComponente)
                .Select(x => new InfoComponenteDTO
                {
                    Id = x.Id,
                    Nombre = x.NombreComponente,
                    Upa = new NameDTO
                    {
                        Id = x.Upa.Id,
                        Nombre = x.Upa.Nombre
                    },
                    Actividad = new NameDTO
                    {
                        Id = x.Actividad.Id,
                        Nombre = x.Actividad.Nombre
                    },
                    Modulo = new NameDTO
                    {
                        Id = x.ModuloComponente.Id,
                        Nombre = x.ModuloComponente.Nombre
                    },
                    EstadoComponente = new EstadoComponenteDTO
                    {
                        Id = x.ObjetoJsonEstado.Id,
                        TipoEstado = x.ObjetoJsonEstado.TipoEstado,
                        ByteHexaFuncion = x.ObjetoJsonEstado.ByteHexaFuncion
                    },
                    DireccionDeRegistro = x.DireccionRegistro
                }).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<NameDTO>> GetComponentesPorUpaId(UpaActivitiesFilterRequest upaActivitiesfilter, bool IsAdmin)
        {
            if (IsAdmin)
            {
                var query = _dbSet.AsNoTracking();
                if (upaActivitiesfilter.UpaId != Guid.Empty)
                {
                    query = query.Where(x => x.UpaId == upaActivitiesfilter.UpaId);
                }
                return await query.Select(x => new NameDTO
                {
                    Id = x.Id,
                    Nombre = x.NombreComponente
                }).OrderBy(y => y.Nombre).ToListAsync();
            }
            return await _dbSet.Where(x => x.UpaId == upaActivitiesfilter.UpaId
            && upaActivitiesfilter.ActividadesId.Contains(x.ActividadId)).Select(x => new NameDTO
            {
                Id = x.Id,
                Nombre = x.NombreComponente
            }).OrderBy(y => y.Nombre).ToListAsync();
        }

        public async Task CreateMultiple(List<EComponenteLaboratorio> entidades)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    await _db.AddRangeAsync(entidades);
                    await _db.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ObtenerException(ex, entidades[0]);
                }
            }
        }

        public async Task<List<int>> GetRegistrationAddressesByUpaModulo(UpaModuleActivityFilterRequest filterRequest, List<int> direccionesRegistro)
        {

            try
            {
                List<byte> direccionesUtilizadas = new List<byte>();
                var estado = await _dbSet.Where(y => y.ModuloComponenteId == filterRequest.ModuloId).Select(y => y.JsonEstadoComponente).FirstOrDefaultAsync();
                if (!string.IsNullOrWhiteSpace(estado))
                {
                    EstadoComponente tipoFuncion = JsonSerializer.Deserialize<EstadoComponente>(estado);

                    if (tipoFuncion.TipoEstado == EnumConfig.GetDescription(TipoEstadoComponente.OnOff))
                    {
                        direccionesUtilizadas = await _dbSet.Where(x => x.UpaId == filterRequest.UpaId).Select(y => y.DireccionRegistro).ToListAsync();
                    }
                    else
                    {
                        direccionesUtilizadas = await _dbSet.Where(x => x.UpaId == filterRequest.UpaId && x.ModuloComponenteId == filterRequest.ModuloId)
                            .Select(y => y.DireccionRegistro).ToListAsync();
                    }
                }
                else
                {
                    direccionesUtilizadas = await _dbSet.Where(x => x.UpaId == filterRequest.UpaId).Select(y => y.DireccionRegistro).ToListAsync();
                }
                var sqlParameters = new List<NpgsqlParameter>();
                var upaId = new NpgsqlParameter("upaId", filterRequest.UpaId);
                var estadoComponente = new NpgsqlParameter("Funcion", EnumConfig.GetDescription(TipoEstadoComponente.OnOff));
                sqlParameters.Add(upaId);
                sqlParameters.Add(estadoComponente);

                string consulta = @"SELECT comp.direccion_registro, comp.upa_id, comp.modulo_componente_id
                                FROM laboratorio_lestoma.componente_laboratorio comp
                                WHERE comp.upa_id = @upaId      
                                  AND comp.descripcion_estado::JSONB ->> 'TipoEstado' = @Funcion";

                var direccionesActuadores = await _dbSet.FromSqlRaw(consulta, sqlParameters.ToArray()).Select(y => y.DireccionRegistro).ToListAsync();

                var direccionesNoUtilizadas = new List<int>();

                if (direccionesActuadores.Any())
                {
                    foreach (var direccionRegistro in direccionesRegistro)
                    {
                        if (!direccionesUtilizadas.Contains((byte)direccionRegistro) && !direccionesActuadores.Contains((byte)direccionRegistro))
                        {
                            direccionesNoUtilizadas.Add(direccionRegistro);
                        }
                    }
                }
                else
                {
                    foreach (var direccionRegistro in direccionesRegistro)
                    {
                        if (!direccionesUtilizadas.Contains((byte)direccionRegistro))
                        {
                            direccionesNoUtilizadas.Add(direccionRegistro);
                        }
                    }
                }
                return direccionesNoUtilizadas;
            }
            catch (Exception ex)
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, @$"Error: {ex.Message}");
            }
        }
    }
}

