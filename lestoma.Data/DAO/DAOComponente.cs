using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lestoma.Data.DAO
{
    public class DAOComponente : GenericRepository<EComponentesLaboratorio>
    {
        private readonly Mapeo _db;

        public DAOComponente(Mapeo db) : base(db)
        {
            _db = db;
        }
        public async Task<bool> ExisteComp(string nombre, Guid id, bool insertOrUpdate = false)
        {
            if (!insertOrUpdate)
            {
                return await _db.TablaComponentesLaboratorio.AnyAsync(x => x.Nombre.ToLower().Equals(nombre.ToLower()));

            }
            else
            {
                return await _db.TablaComponentesLaboratorio.AnyAsync(x => x.Nombre.ToLower().Equals(nombre.ToLower()) && x.Id != id);

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
            var comp = _db.TablaComponentesLaboratorio.FromSqlRaw("SELECT id, nombre FROM laboratorio_lestoma.componente_laboratorio").OrderBy(x => x.Nombre);
            var query = comp.Select(x => new NameDTO
            {
                Id = x.Id,
                Nombre = x.Nombre,

            }).ToList();
            return query;

        }

    }
}

