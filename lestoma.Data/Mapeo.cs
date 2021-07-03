using lestoma.CommonUtils.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lestoma.Data
{
    public class Mapeo : DbContext
    {
        public Mapeo() { }
        public Mapeo(DbContextOptions<Mapeo> options) : base(options) { }

        public DbSet<EUsuario> TablaUsuarios { get; set; }
        public DbSet<EEstadoUsuario> TablaEstadosUsuarios { get; set; }
        public DbSet<ERol> TablaRoles { get; set; }
        public DbSet<EBuzon> TablaBuzonReportes { get; set; }
    }
}
