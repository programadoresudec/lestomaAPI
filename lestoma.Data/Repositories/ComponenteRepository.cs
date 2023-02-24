using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Requests.Filters;
using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (upaActivitiesFilter.ActividadesId != null)
            {
                if (upaActivitiesFilter.ActividadesId.Any())
                {
                    query = query.Where(x => upaActivitiesFilter.ActividadesId.Contains(x.ActividadId));
                }
            }
            var listado = query.Select(x => new ListadoComponenteDTO
            {
                Actividad = x.Actividad.Nombre,
                Nombre = x.NombreComponente,
                FechaCreacionServer = x.FechaCreacionServer,
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
                        ByteHexaFuncion = x.ObjetoJsonEstado.ByteFuncion
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
                }).ToListAsync();
            }
            return await _dbSet.Where(x => x.UpaId == upaActivitiesfilter.UpaId && upaActivitiesfilter.ActividadesId.Contains(x.ActividadId)).Select(x => new NameDTO
            {
                Id = x.Id,
                Nombre = x.NombreComponente
            }).ToListAsync();
        }
    }
}

