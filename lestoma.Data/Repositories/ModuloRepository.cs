using lestoma.CommonUtils.DTOs;
using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Data.Repositories
{
    public class ModuloRepository : GenericRepository<EModuloComponente>
    {
        private readonly LestomaContext _db;
        public ModuloRepository(LestomaContext db) : base(db)
        {
            _db = db;
        }
        public async Task<bool> ExisteModulo(string nombre, Guid id, bool insertOrUpdate = false)
        {
            if (!insertOrUpdate && id == Guid.Empty)
            {
                return await _dbSet.AnyAsync(x => x.Nombre.ToLower().Trim().Equals(nombre.ToLower().Trim()));
            }
            else
            {
                return await _dbSet.AnyAsync(x => x.Nombre.ToLower().Trim().
                Equals(nombre.ToLower().Trim()) && x.Id != id);
            }

        }

        public async Task<IEnumerable<NameDTO>> GetModulosJustNames()
        {
            var modulos = await _dbSet.Select(x => new NameDTO
            {
                Id = x.Id,
                Nombre = x.Nombre
            }).ToListAsync();
            return modulos;
        }
    }
}