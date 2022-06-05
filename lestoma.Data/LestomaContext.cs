
using lestoma.CommonUtils.Interfaces;
using lestoma.Entidades.Models;
using lestoma.Entidades.ModelsReports;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace lestoma.Data
{
    public class LestomaContext : DbContext
    {
        private readonly ICamposAuditoriaHelper _camposAuditoria;
        public LestomaContext() { }


        #region Constructor conexion Postgres
        public LestomaContext(DbContextOptions<LestomaContext> options, ICamposAuditoriaHelper camposAuditoria)
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
            ProcesarAuditoria();
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
        public DbSet<EComponentesLaboratorio> TablaComponentesLaboratorio { get; set; }
        public DbSet<EAlimentarPeces> TablaReporteAlimentarPeces { get; set; }
        public DbSet<EControlAgua> TablaReporteControlAgua { get; set; }
        public DbSet<EControlElectrico> TablaReporteControlElectrico { get; set; }
        public DbSet<EControlEntorno> TablaReporteControlEntorno { get; set; }
        public DbSet<EControlHidroponico> TablaReporteControlHidroponico { get; set; }
        public DbSet<ERecirculacionAgua> TablaReporteRecirculacionAgua { get; set; }

        #endregion

        #region Mapeo en la base de datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            #region Schema superadmin
            modelBuilder.Entity<EActividad>().ToTable("actividad", "superadmin");
            modelBuilder.Entity<EUpa>().ToTable("upa", "superadmin");
            modelBuilder.Entity<EUpaActividad>().ToTable("upa_actividad", "superadmin")
           .HasKey(x => new { x.UpaId, x.ActividadId, x.UsuarioId });
            #endregion

            #region Schema usuarios
            modelBuilder.Entity<EUsuario>().ToTable("usuario", "usuarios");
            modelBuilder.Entity<ERol>()
                .HasMany(g => g.Usuarios)
                .WithOne(s => s.Rol)
                .HasForeignKey(s => s.RolId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<EEstadoUsuario>()
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
            modelBuilder.Entity<EComponentesLaboratorio>().ToTable("componente_laboratorio", "laboratorio_lestoma");
            modelBuilder.Entity<ELaboratorio>().ToTable("detalle_laboratorio", "laboratorio_lestoma");
            modelBuilder.Entity<EModuloComponente>().ToTable("modulo_componente", "laboratorio_lestoma");
            modelBuilder.Entity<EProtocoloCOM>().ToTable("protocolo_com", "laboratorio_lestoma");
            #endregion

            #region Schema Reportes
            modelBuilder.Entity<EBuzon>().ToTable("buzon", "reportes");
            modelBuilder.Entity<EAlimentarPeces>().ToTable("alimentar_peces", "reportes");
            modelBuilder.Entity<EControlAgua>().ToTable("control_de_agua", "reportes");
            modelBuilder.Entity<EControlElectrico>().ToTable("control_electrico", "reportes");
            modelBuilder.Entity<EControlEntorno>().ToTable("control_de_entorno", "reportes");
            modelBuilder.Entity<EControlHidroponico>().ToTable("control_hidroponico", "reportes");
            modelBuilder.Entity<ERecirculacionAgua>().ToTable("recirculacion_de_agua", "reportes");

            #endregion

            SeeData(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private static void SeeData(ModelBuilder modelBuilder)
        {
            #region data roles
            var superAdmin = new ERol() { Id = 1, NombreRol = "Super Administrador" };
            var admin = new ERol() { Id = 2, NombreRol = "Administrador" };
            var auxiliar = new ERol() { Id = 3, NombreRol = "Auxiliar" };

            modelBuilder.Entity<ERol>()
                .HasData(new List<ERol>
                {
                    superAdmin,admin,auxiliar
                });
            #endregion

            #region data estados usuario
            var checkCuenta = new EEstadoUsuario() { Id = 1, DescripcionEstado = "verificar cuenta" };
            var Activado = new EEstadoUsuario() { Id = 2, DescripcionEstado = "Activado" };
            var Inactivo = new EEstadoUsuario() { Id = 3, DescripcionEstado = "Inactivo" };
            var Bloqueado = new EEstadoUsuario() { Id = 4, DescripcionEstado = "Bloqueado" };

            modelBuilder.Entity<EEstadoUsuario>()
                .HasData(new List<EEstadoUsuario>
                {
                   checkCuenta, Activado, Inactivo,Bloqueado
                });
            #endregion

            #region data aplicaciones
            var app = new EAplicacion() { Id = 1, NombreAplicacion = "App Movil", TiempoExpiracionToken = 31 };
            var web = new EAplicacion() { Id = 2, NombreAplicacion = "Web", TiempoExpiracionToken = 45 };

            modelBuilder.Entity<EAplicacion>()
                .HasData(new List<EAplicacion>
                {
                    app,web
                });
            #endregion

            #region data protocolo
            var peerToPeer = new EProtocoloCOM() { Id = 1, Nombre = "Peer to Peer", Sigla = "PP", PrimerByteTrama = "49" };
            var broadCast = new EProtocoloCOM() { Id = 2, Nombre = "Broad Cast", Sigla = "BS", PrimerByteTrama = "6F" };

            modelBuilder.Entity<EProtocoloCOM>()
                .HasData(new List<EProtocoloCOM>
                {
                    peerToPeer,broadCast
                });
            #endregion
        }
        #endregion
        public void ProcesarAuditoria()
        {
            foreach (var item in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added && e.Entity is ECamposAuditoria))
            {
                var entidad = item.Entity as ECamposAuditoria;
                entidad.Ip = _camposAuditoria.ObtenerIp();
                entidad.Session = _camposAuditoria.ObtenerUsuarioActual();
                entidad.TipoDeAplicacion = _camposAuditoria.ObtenerTipoDeAplicacion();
                entidad.FechaCreacionServer = DateTime.Now;
            }

            foreach (var item in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified && e.Entity is ECamposAuditoria))
            {
                var entidad = item.Entity as ECamposAuditoria;
                entidad.Ip = _camposAuditoria.ObtenerIp();
                entidad.Session = _camposAuditoria.ObtenerUsuarioActual();
                entidad.TipoDeAplicacion = _camposAuditoria.ObtenerTipoDeAplicacion();
                entidad.FechaCreacionServer = DateTime.Now;
            }
        }
    }
}
