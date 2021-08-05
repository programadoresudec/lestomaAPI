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

        #region DBSET tablas 
        public DbSet<EUsuario> TablaUsuarios { get; set; }
        public DbSet<EEstadoUsuario> TablaEstadosUsuarios { get; set; }
        public DbSet<ERol> TablaRoles { get; set; }
        public DbSet<EBuzon> TablaBuzonReportes { get; set; }
        public DbSet<EAuditoria> TablaAuditoria { get; set; }
        public DbSet<EUpa> TablaUpas { get; set; }
        public DbSet<EActividad> TablaActividades { get; set; }
        public DbSet<EUpaActividad> TablaUpasConActividades { get; set; }
        #endregion

        #region Mapeo en la base de datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EActividad>().ToTable("actividad", "superadmin");
            modelBuilder.Entity<EUpa>().ToTable("upa", "superadmin");
            modelBuilder.Entity<EUpaActividad>().ToTable("upa_actividad", "superadmin")
           .HasKey(x => new { x.UpaId, x.ActividadId });
            modelBuilder.Entity<EUsuario>().ToTable("usuario", "usuarios");
            modelBuilder.Entity<EAuditoria>().ToTable("auditoria", "seguridad");
            modelBuilder.Entity<ERol>().ToTable("rol", "usuarios");
            modelBuilder.Entity<EEstadoUsuario>().ToTable("estado_usuario", "usuarios");
            modelBuilder.Entity<EBuzon>().ToTable("buzon", "reportes");
            base.OnModelCreating(modelBuilder);
        } 
        #endregion
    }
}
