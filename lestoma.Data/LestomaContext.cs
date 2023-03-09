using lestoma.CommonUtils.Interfaces;
using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace lestoma.Data
{
    public class LestomaContext : DbContext
    {
        private readonly IAuditoriaHelper _camposAuditoria;
        public LestomaContext() { }


        #region Constructor conexion Postgres
        public LestomaContext(DbContextOptions<LestomaContext> options, IAuditoriaHelper camposAuditoria)
         : base(options)
        {
            _camposAuditoria = camposAuditoria ?? throw new ArgumentNullException(nameof(camposAuditoria));
        }

        #endregion
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ProcesarAuditoria();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {

            ProcesarAuditoria(acceptAllChangesOnSuccess);
            acceptAllChangesOnSuccess = true;
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
        public DbSet<ELaboratorio> TablaDetalleLaboratorio { get; set; }
        public DbSet<EModuloComponente> TablaModuloComponentes { get; set; }
        public DbSet<EProtocoloCOM> TablaProtocoloCOM { get; set; }
        public DbSet<EComponenteLaboratorio> TablaComponentesLaboratorio { get; set; }

        #endregion

        #region Mapeo en la base de datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Schema superadmin
            modelBuilder.Entity<EActividad>().ToTable("actividad", "superadmin");
            modelBuilder.Entity<EUpa>().ToTable("upa", "superadmin").HasMany(g => g.ProtocolosCOM)
                .WithOne(s => s.Upa)
                .HasForeignKey(s => s.UpaId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<EUpaActividad>().ToTable("upa_actividad", "superadmin")
           .HasKey(x => new { x.UpaId, x.ActividadId, x.UsuarioId });
            #endregion

            #region Schema usuarios
            modelBuilder.Entity<EUsuario>().ToTable("usuario", "usuarios");
            modelBuilder.Entity<ERol>().ToTable("rol", "usuarios")
                .HasMany(g => g.Usuarios)
                .WithOne(s => s.Rol)
                .HasForeignKey(s => s.RolId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<EEstadoUsuario>().ToTable("estado_usuario", "usuarios")
                .HasMany(g => g.Usuarios)
                .WithOne(s => s.EstadoUsuario)
                .HasForeignKey(s => s.EstadoId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Schema Seguridad
            modelBuilder.Entity<EUsuario>().OwnsMany(
            p => p.RefreshTokens, a =>
            {
                a.WithOwner().HasForeignKey("usuario_id");
                a.Property<int>("Id");
                a.HasKey("Id");
            });
            modelBuilder.Entity<EAuditoria>().ToTable("auditoria", "seguridad");
            modelBuilder.Entity<EAplicacion>().ToTable("aplicacion", "seguridad");
            #endregion

            #region Schema laboratorio
            modelBuilder.Entity<EComponenteLaboratorio>().ToTable("componente_laboratorio", "laboratorio_lestoma");
            modelBuilder.Entity<ELaboratorio>().ToTable("detalle_laboratorio", "laboratorio_lestoma");
            modelBuilder.Entity<EModuloComponente>().ToTable("modulo_componente", "laboratorio_lestoma");
            modelBuilder.Entity<EProtocoloCOM>().ToTable("protocolo_com", "laboratorio_lestoma");
            #endregion

            #region Schema buzon de Reportes
            modelBuilder.Entity<EBuzon>().ToTable("buzon", "reportes");
            #endregion

            /// restringe el eliminar en cascada
            var cascadeFks = modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var item in cascadeFks)
            {
                item.DeleteBehavior = DeleteBehavior.Restrict;
            }

            /// guarda data generica al hacer migration de database
            SeedData.SaveData(modelBuilder, _camposAuditoria.GetDesencrytedIp(), _camposAuditoria.GetTipoDeAplicacion(), _camposAuditoria.GetSession());

            base.OnModelCreating(modelBuilder);
        }
        #endregion

        #region Auditoria de tablas
        public void ProcesarAuditoria(bool acceptAllChangesOnSuccess = true)
        {
            if (!acceptAllChangesOnSuccess)
            {
                foreach (var item in ChangeTracker.Entries()
                               .Where(e => e.State == EntityState.Added && e.Entity is ECamposAuditoria))
                {
                    var entidad = item.Entity as ECamposAuditoria;
                    entidad.Ip = _camposAuditoria.GetDesencrytedIp();
                    entidad.Session = _camposAuditoria.GetSession();
                    entidad.TipoDeAplicacion = _camposAuditoria.GetTipoDeAplicacion();
                }
            }
            else
            {
                foreach (var item in ChangeTracker.Entries()
                           .Where(e => e.State == EntityState.Added && e.Entity is ECamposAuditoria))
                {
                    var entidad = item.Entity as ECamposAuditoria;
                    entidad.Ip = _camposAuditoria.GetDesencrytedIp();
                    entidad.Session = _camposAuditoria.GetSession();
                    entidad.TipoDeAplicacion = _camposAuditoria.GetTipoDeAplicacion();
                    entidad.FechaCreacionServer = DateTime.Now;
                }

                foreach (var item in ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Modified && e.Entity is ECamposAuditoria))
                {
                    var entidad = item.Entity as ECamposAuditoria;
                    entidad.Ip = _camposAuditoria.GetDesencrytedIp();
                    entidad.Session = _camposAuditoria.GetSession();
                    entidad.TipoDeAplicacion = _camposAuditoria.GetTipoDeAplicacion();
                    entidad.FechaActualizacionServer = DateTime.Now;
                }
            }
        }
        #endregion
    }
}
