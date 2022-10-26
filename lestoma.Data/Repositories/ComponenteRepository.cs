using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public List<NameDTO> GetComponentesJustNames()
        {
            var comp = _dbSet.FromSqlRaw("SELECT id, nombre FROM laboratorio_lestoma.componente_laboratorio").OrderBy(x => x.NombreComponente);
            var query = comp.Select(x => new NameDTO
            {
                Id = x.Id,
                Nombre = x.NombreComponente,

            }).ToList();
            return query;

        }

        public IQueryable<ListadoComponenteDTO> GetAllFilter(Guid upaId)
        {
            var query = _dbSet.AsNoTracking();
            if (upaId != Guid.Empty)
            {
                query = query.Where(x => x.UpaId == upaId);
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
                Upa = x.Upa.Nombre
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
                        ByteFuncion = x.ObjetoJsonEstado.ByteFuncion
                    }
                }).FirstOrDefaultAsync();
        }
    }
}

