using lestoma.CommonUtils.DTOs;
using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace lestoma.Data.DAO
{
    public class DAOComponente : GenericRepository<EComponentesLaboratorio>
    {
        private readonly LestomaContext _db;

        public DAOComponente(LestomaContext db) : base(db)
        {
            _db = db;
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

