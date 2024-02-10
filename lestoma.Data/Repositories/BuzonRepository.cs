using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Requests;
using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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

        public async Task<DetalleBuzonDTO> GetMailBoxById(int id)
        {
            var data = await _db.TablaBuzonReportes.Where(x => x.Id == id).Select(y => y.Descripcion)
                .FirstOrDefaultAsync();
            if (data == null)
                return null;
            return JsonSerializer.Deserialize<DetalleBuzonDTO>(data);
        }

        public IQueryable<BuzonDTO> ListarBuzonConUsuario(Guid UpaId)
        {
            var lista = from buzon in _db.TablaBuzonReportes
                        join user in _db.TablaUsuarios on buzon.UsuarioId equals user.Id
                        select new
                        {
                            buzon,
                            user
                        };

            var query = lista.Select(m => new BuzonDTO

            {
                Id = m.buzon.Id,
                User = new UserDTO
                {
                    Id = m.user.Id,
                    Nombre = m.user.Nombre,
                    Apellido = m.user.Apellido
                },
                FechaCreacionServer = m.buzon.FechaCreacionServer,
                FechaActualizacionServer = m.buzon.FechaActualizacionServer,
                Ip = m.buzon.Ip,
                Session = m.buzon.Session,
                TipoDeAplicacion = m.buzon.TipoDeAplicacion,
                Titulo = _db.TablaBuzonReportes.FromSqlRaw(@"SELECT buzon.id, buzon.descripcion::JSONB->>'Titulo' as descripcion FROM reportes.buzon buzon")
                .Where(x => x.Id == m.buzon.Id).Select(x => x.Descripcion).FirstOrDefault(),
                EstadoId = Convert.ToInt32(_db.TablaBuzonReportes.
                                            FromSqlRaw(@"SELECT buzon.id, buzon.descripcion::JSONB->'Estado'->>'Id' AS descripcion FROM reportes.buzon buzon")
                .Where(x => x.Id == m.buzon.Id).Select(x => x.Descripcion).FirstOrDefault()),
                Upa = (from upa in _db.TablaUpas
                       join detalle in _db.TablaUpasConActividades on upa.Id equals detalle.UpaId
                       where m.user.Id == detalle.UsuarioId
                       select new NameDTO { Id = upa.Id, Nombre = upa.Nombre }).FirstOrDefault()
            });

            if (UpaId != Guid.Empty)
            {
                query = query.Where(x => x.Upa.Id == UpaId);
            }

            return query.OrderByDescending(x => x.FechaActualizacionServer).AsNoTracking();
        }
    }
}
