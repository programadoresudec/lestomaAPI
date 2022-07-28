using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Data.Repositories
{
    public class BuzonRepository : GenericRepository<EBuzon>
    {
        private readonly LestomaContext _db;
        public BuzonRepository(LestomaContext db) : base(db)
        {
            _db = db;
        }
        public IQueryable<EBuzon> ListarBuzonConUsuario()
        {
            var lista = from buzon in _db.TablaBuzonReportes
                        join user in _db.TablaUsuarios on buzon.UsuarioId equals user.Id
                        select new
                        {
                            buzon,
                            user
                        };

            var query =  lista.Select(m => new EBuzon

            {
                Descripcion = m.buzon.Descripcion,
                FechaCreacionServer = m.buzon.FechaCreacionServer,
                Id = m.buzon.Id,
                User =
                {
                    Nombre = m.user.Nombre,
                    Apellido = m.user.Apellido
                }
            }).AsNoTracking();
            return query;
        }
    }
}
