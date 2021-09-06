
using lestoma.CommonUtils.Interfaces;
using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace lestoma.Data
{
    public class Mapeo : DbContext
    {
        private readonly ICamposAuditoriaHelper _camposAuditoria;
        public Mapeo() { }
        public Mapeo(DbContextOptions<Mapeo> options, ICamposAuditoriaHelper camposAuditoria)
            : base(options)
        {
            _camposAuditoria = camposAuditoria ?? throw new ArgumentNullException(nameof(camposAuditoria));
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ProcesarSalvado();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        #region DBSET tablas 
        public DbSet<EUsuario> TablaUsuarios { get; set; }
        public DbSet<EEstadoUsuario> TablaEstadosUsuarios { get; set; }
        public DbSet<ERol> TablaRoles { get; set; }
        public DbSet<EBuzon> TablaBuzonReportes { get; set; }
        public DbSet<EAuditoria> TablaAuditoria { get; set; }
        public DbSet<EUpa> TablaUpas { get; set; }
        public DbSet<EActividad> TablaActividades { get; set; }
        public DbSet<EUpaActividad> TablaUpasConActividades { get; set; }
        public DbSet<EAplicacion> TablaAplicaciones { get; set; }
        public DbSet<ESuperAdministrador> TablaSuperAdministradores { get; set; }
        #endregion

        #region Mapeo en la base de datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EActividad>().ToTable("actividad", "superadmin");
            modelBuilder.Entity<EUpa>().ToTable("upa", "superadmin");
            modelBuilder.Entity<EUpaActividad>().ToTable("upa_actividad", "superadmin")
           .HasKey(x => new { x.UpaId, x.ActividadId });
            modelBuilder.Entity<EUsuario>().ToTable("usuario", "usuarios");
            modelBuilder.Entity<EUsuario>().OwnsMany(
                p => p.RefreshTokens, a =>
                {
                    a.WithOwner().HasForeignKey("usuario_id");
                    a.Property<int>("Id");
                    a.HasKey("Id");
                });

            modelBuilder.Entity<ERol>()
                .HasMany<EUsuario>(g => g.Usuarios)
                .WithOne(s => s.Rol)
                .HasForeignKey(s => s.RolId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EEstadoUsuario>()
                .HasMany<EUsuario>(g => g.Usuarios)
                .WithOne(s => s.EstadoUsuario)
                .HasForeignKey(s => s.EstadoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EAuditoria>().ToTable("auditoria", "seguridad");
            modelBuilder.Entity<ERol>().ToTable("rol", "usuarios");
            modelBuilder.Entity<EEstadoUsuario>().ToTable("estado_usuario", "usuarios");
            modelBuilder.Entity<EAplicacion>().ToTable("aplicacion", "seguridad");
            modelBuilder.Entity<EBuzon>().ToTable("buzon", "reportes");
            base.OnModelCreating(modelBuilder);
        }
        #endregion
        private void ProcesarSalvado()
        {
            foreach (var item in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added && e.Entity is ECamposAuditoria))
            {
                var entidad = item.Entity as ECamposAuditoria;
                entidad.Ip = _camposAuditoria.ObtenerIp();
                entidad.Session = _camposAuditoria.ObtenerUsuarioActual();
                entidad.TipoDeAplicacion = _camposAuditoria.ObtenerTipoDeAplicacion();
            }

            foreach (var item in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified && e.Entity is ECamposAuditoria))
            {
                var entidad = item.Entity as ECamposAuditoria;
                entidad.Ip = _camposAuditoria.ObtenerIp();
                entidad.Session = _camposAuditoria.ObtenerUsuarioActual();
                entidad.TipoDeAplicacion = _camposAuditoria.ObtenerTipoDeAplicacion();
            }
        }
    }
}
