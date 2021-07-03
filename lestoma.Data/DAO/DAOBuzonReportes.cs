using lestoma.CommonUtils.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lestoma.Data.DAO
{
    public class DAOBuzonReportes
    {
        public async Task<List<EBuzon>> ListarBuzonConUsuario(Mapeo db)
        {
            var lista = (await (from buzon in db.TablaBuzonReportes
                                join user in db.TablaUsuarios on buzon.UsuarioId equals user.Id
                                select new
                                {
                                    buzon,
                                    user
                                }).ToListAsync());

            return lista.Select(m => new EBuzon

            {
                Descripcion = m.buzon.Descripcion,
                FechaCreacion = m.buzon.FechaCreacion,
                Id = m.buzon.Id,
                User =
                {
                    Nombre = m.user.Nombre,
                    Apellido = m.user.Apellido
                }
            }).ToList();
        }
    }
}
