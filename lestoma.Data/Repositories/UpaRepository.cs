using lestoma.CommonUtils.DTOs;
using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Data.Repositories
{
    public class UpaRepository : GenericRepository<EUpa>
    {
        private readonly LestomaContext _db;
        public UpaRepository(LestomaContext db) : base(db)
        {
            _db = db;
        }

        public async Task DeleteInCascade(EUpa entidad)
        {
            var protocolos = await _db.TablaProtocoloCOM.Where(x => x.UpaId == entidad.Id).ToListAsync();
            if (protocolos.Any())
                _db.RemoveRange(protocolos);
            await Delete(entidad);
        }

        public async Task<bool> ExisteUpa(string nombre, Guid id, bool insertOrUpdate = false)
        {
            if (!insertOrUpdate)
            {
                return await _dbSet.AnyAsync(x => x.Nombre.ToLower().Equals(nombre.ToLower()));
            }
            else
            {
                return await _dbSet.AnyAsync(x => x.Nombre.ToLower().Equals(nombre.ToLower()) && x.Id != id);
            }

        }
        public async Task<ESuperAdministrador> GetSuperAdmin(int userId)
        {
            return await _db.TablaSuperAdministradores.FirstOrDefaultAsync(x => x.UsuarioId == userId);
        }

        public async Task<IEnumerable<NameDTO>> GetUpasJustNames()
        {
            var query = await _dbSet.Select(x => new NameDTO
            {
                Id = x.Id,
                Nombre = x.Nombre,
            }).OrderBy(x => x.Nombre).ToListAsync();
            return query;
        }
    }
}
