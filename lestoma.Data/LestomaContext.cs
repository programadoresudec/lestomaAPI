
using lestoma.CommonUtils.Constants;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Interfaces;
using lestoma.Entidades.Models;
using lestoma.Entidades.ModelsReports;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
        public DbSet<EComponenteLaboratorio> TablaComponentesLaboratorio { get; set; }
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
            modelBuilder.Entity<EComponenteLaboratorio>().ToTable("componente_laboratorio", "laboratorio_lestoma");
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

        private void SeeData(ModelBuilder modelBuilder)
        {

            var ip = _camposAuditoria.ObtenerIp();
            var aplicacion = _camposAuditoria.ObtenerTipoDeAplicacion();
            var usersession = _camposAuditoria.ObtenerUsuarioActual();


            #region data roles
            var RolSuperAdmin = new ERol() { Id = (int)TipoRol.SuperAdministrador, NombreRol = "Super Administrador" };
            var RolAdmin = new ERol() { Id = (int)TipoRol.Administrador, NombreRol = "Administrador" };
            var RolAuxiliar = new ERol() { Id = (int)TipoRol.Auxiliar, NombreRol = "Auxiliar" };

            modelBuilder.Entity<ERol>()
                .HasData(new List<ERol>
                {
                    RolSuperAdmin,RolAdmin,RolAuxiliar
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

            #region data modulos
            var actuador = new EModuloComponente()
            {
                Id = Guid.NewGuid(),
                Nombre = "ACTUADORES",
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion
            };
            var sensor = new EModuloComponente()
            {
                Id = Guid.NewGuid(),
                Nombre = "SENSORES",
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion
            };

            modelBuilder.Entity<EModuloComponente>()
                .HasData(new List<EModuloComponente>
                {
                    actuador,sensor
                });
            #endregion

            #region data usuarios
            var hashSuper = HashHelper.Hash(Constants.PASSWORD_SUPER_ADMIN);
            var superadmin = new EUsuario
            {
                Id = 1,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion,
                Nombre = "Super Admin",
                Apellido = "Lestoma",
                Clave = hashSuper.Password,
                Salt = hashSuper.Salt,
                EstadoId = (int)TipoEstadoUsuario.Activado,
                RolId = (int)TipoRol.SuperAdministrador,
                Email = Constants.EMAIL_SUPER_ADMIN
            };

            var hashAdmin = HashHelper.Hash(Constants.PASSWORD_ADMIN);
            var administrador = new EUsuario
            {
                Id = 2,
                Nombre = "Administrador",
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion,
                Apellido = "Lestoma",
                Clave = hashAdmin.Password,
                Salt = hashAdmin.Salt,
                EstadoId = (int)TipoEstadoUsuario.Activado,
                RolId = (int)TipoRol.Administrador,
                Email = Constants.EMAIL_ADMIN
            };

            var hashAuxiliar = HashHelper.Hash(Constants.PASSWORD_AUXILIAR);
            var auxiliar1 = new EUsuario
            {
                Id = 3,
                Nombre = "Auxiliar 1",
                Apellido = "Lestoma",
                Clave = hashAuxiliar.Password,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion,
                Salt = hashAuxiliar.Salt,
                EstadoId = (int)TipoEstadoUsuario.Activado,
                RolId = (int)TipoRol.Auxiliar,
                Email = Constants.EMAIL_AUXILIAR
            };
            var auxiliar2 = new EUsuario
            {
                Id = 4,
                Nombre = "Auxiliar 2",
                Apellido = "Lestoma",
                Clave = hashAuxiliar.Password,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion,
                Salt = hashAuxiliar.Salt,
                EstadoId = (int)TipoEstadoUsuario.Activado,
                RolId = (int)TipoRol.Auxiliar,
                Email = "auxiliar2@gmail.com"
            };

            modelBuilder.Entity<EUsuario>()
                .HasData(new List<EUsuario>
                {
                    superadmin, administrador, auxiliar1, auxiliar2
                });

            var super = new ESuperAdministrador
            {
                Id = 1,
                UsuarioId = 1,
            };
            modelBuilder.Entity<ESuperAdministrador>()
             .HasData(new List<ESuperAdministrador>
             {
                    super
             });
            #endregion

            #region data upas

            var upa1 = new EUpa()
            {
                Id = Guid.NewGuid(),
                Nombre = "finca el vergel",
                Descripcion = "queda ubicada en facatativá",
                CantidadActividades = 5,
                SuperAdminId = super.Id,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion
            };
            var upa2 = new EUpa()
            {
                Id = Guid.NewGuid(),
                Nombre = "ucundinamarca",
                Descripcion = "queda ubicada en la universidad cundinamarca extensión nfaca",
                CantidadActividades = 2,
                SuperAdminId = super.Id,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion,
            };

            modelBuilder.Entity<EUpa>()
                .HasData(new List<EUpa>
                {
                    upa1,upa2
                });
            #endregion

            #region data actividades
            var controlAgua = new EActividad()
            {
                Id = Guid.NewGuid(),
                Nombre = "control de agua",
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion,
            };
            var alimentacionPeces = new EActividad()
            {
                Id = Guid.NewGuid(),
                Nombre = "alimentacion de peces",
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion
            };

            modelBuilder.Entity<EActividad>()
                .HasData(new List<EActividad>
                {
                    controlAgua,alimentacionPeces
                });
            #endregion

            #region data detalle upa-actividad
            var detalle1_1 = new EUpaActividad()
            {
                UpaId = upa1.Id,
                ActividadId = controlAgua.Id,
                UsuarioId = administrador.Id,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion
            };
            var detalle1_2 = new EUpaActividad()
            {
                UpaId = upa1.Id,
                ActividadId = alimentacionPeces.Id,
                UsuarioId = administrador.Id,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion
            };

            var detalle1UserAuxiliar = new EUpaActividad()
            {
                UpaId = upa1.Id,
                ActividadId = controlAgua.Id,
                UsuarioId = auxiliar1.Id,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion
            };
            var detalle1UserAuxiliar2 = new EUpaActividad()
            {
                UpaId = upa1.Id,
                ActividadId = alimentacionPeces.Id,
                UsuarioId = auxiliar2.Id,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion
            };

            var detalle2_1 = new EUpaActividad()
            {
                UpaId = upa2.Id,
                ActividadId = controlAgua.Id,
                UsuarioId = administrador.Id,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion
            };
            var detalle2UserAuxiliar1 = new EUpaActividad()
            {
                UpaId = upa2.Id,
                ActividadId = controlAgua.Id,
                UsuarioId = auxiliar1.Id,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion
            };
            var detalle2UserAuxiliar2 = new EUpaActividad()
            {
                UpaId = upa2.Id,
                ActividadId = controlAgua.Id,
                UsuarioId = auxiliar2.Id,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion
            };


            modelBuilder.Entity<EUpaActividad>()
                .HasData(new List<EUpaActividad>
                {
                    detalle1_1,detalle1_2,detalle1UserAuxiliar,detalle1UserAuxiliar2,detalle2_1,detalle2UserAuxiliar1,detalle2UserAuxiliar2
                });
            #endregion

            #region data componentes
            var jsonEstadoOnOff = JsonSerializer.Serialize(new EstadosComponentes()
            {

                Id = Guid.NewGuid(),
                TipoEstado = "ON-OFF",
                TercerByteTrama = "F0",
                CuartoByteTrama = "00",

            });


            var jsonEstadoLectura = JsonSerializer.Serialize(new EstadosComponentes()
            {

                Id = Guid.NewGuid(),
                TipoEstado = "LECTURA",
                TercerByteTrama = "0F",
                CuartoByteTrama = "00",
            });

            var bombaDeOxigeno = new EComponenteLaboratorio()
            {
                Id = Guid.NewGuid(),
                NombreComponente = "BOMBA DE OXIGENO",
                ActividadId = alimentacionPeces.Id,
                JsonEstadoComponente = jsonEstadoOnOff,
                UpaId = upa1.Id,
                ModuloComponenteId = actuador.Id,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion
            };

            var luzEstanque = new EComponenteLaboratorio()
            {
                Id = Guid.NewGuid(),
                NombreComponente = "LUZ ESTANQUE",
                ActividadId = alimentacionPeces.Id,
                JsonEstadoComponente = jsonEstadoOnOff,
                UpaId = upa1.Id,
                ModuloComponenteId = actuador.Id,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion
            };

            var dosificadorAlimento = new EComponenteLaboratorio()
            {
                Id = Guid.NewGuid(),
                NombreComponente = "DOSIFICADOR DE ALIMENTO",
                ActividadId = alimentacionPeces.Id,
                JsonEstadoComponente = jsonEstadoOnOff,
                UpaId = upa1.Id,
                ModuloComponenteId = actuador.Id,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion
            };

            var temperaturaH20 = new EComponenteLaboratorio()
            {
                Id = Guid.NewGuid(),
                NombreComponente = "TEMPERATURA H2O",
                ActividadId = controlAgua.Id,
                JsonEstadoComponente = jsonEstadoLectura,
                UpaId = upa1.Id,
                ModuloComponenteId = sensor.Id,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion
            };

            var PH = new EComponenteLaboratorio()
            {
                Id = Guid.NewGuid(),
                NombreComponente = "PH",
                ActividadId = controlAgua.Id,
                JsonEstadoComponente = jsonEstadoLectura,
                UpaId = upa1.Id,
                ModuloComponenteId = sensor.Id,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion
            };

            var nivelTanque = new EComponenteLaboratorio()
            {
                Id = Guid.NewGuid(),
                NombreComponente = "NIVEL TANQUE",
                ActividadId = controlAgua.Id,
                JsonEstadoComponente = jsonEstadoLectura,
                UpaId = upa1.Id,
                ModuloComponenteId = sensor.Id,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion
            };


            modelBuilder.Entity<EComponenteLaboratorio>()
                .HasData(new List<EComponenteLaboratorio>
                {
                    bombaDeOxigeno,luzEstanque,dosificadorAlimento,temperaturaH20,PH,nivelTanque
                });
            #endregion

            #region data detalle laboratorio
            var detalle1 = new ELaboratorio()
            {
                Id = Guid.NewGuid(),
                ComponenteLaboratorioId = bombaDeOxigeno.Id,
                TramaEnviada = "4901F000000000006180",
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion,
                FechaCreacionDispositivo = DateTime.Now,
                EstadoInternet = true,
                TipoDeComunicacionId = peerToPeer.Id,
            };
            var detalle2 = new ELaboratorio()
            {
                Id = Guid.NewGuid(),
                ComponenteLaboratorioId = luzEstanque.Id,
                TramaEnviada = "6F01F000000000005302",
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion,
                FechaCreacionDispositivo = DateTime.Now,
                EstadoInternet = true,
                TipoDeComunicacionId = broadCast.Id,
            };

            modelBuilder.Entity<ELaboratorio>()
                .HasData(new List<ELaboratorio>
                {
                    detalle1,detalle2
                });
            #endregion
        }
        #endregion

        #region Auditoria de tablas
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
        #endregion
    }
}
