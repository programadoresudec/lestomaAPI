using lestoma.CommonUtils.Constants;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.ListadosJson;
using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace lestoma.Data
{
    public abstract class SeedData
    {
        #region Data generica
        public static void SaveData(ModelBuilder modelBuilder, string ip, string aplicacion, string usersession)
        {

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

            var ajuste = new EModuloComponente()
            {
                Id = Guid.NewGuid(),
                Nombre = "SET_POINT/CONTROL",
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion
            };

            modelBuilder.Entity<EModuloComponente>()
                .HasData(new List<EModuloComponente>
                {
                    actuador,sensor,ajuste
                });
            #endregion

            #region data usuarios
            var hashSuperLestoma = HashHelper.Hash(Constants.PWD_SUPER_ADMIN);
            var superadminLestoma = new EUsuario
            {
                Id = 1,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion,
                Nombre = "Lestoma-APP",
                Apellido = "Movil",
                Clave = hashSuperLestoma.Password,
                Salt = hashSuperLestoma.Salt,
                EstadoId = (int)TipoEstadoUsuario.Activado,
                RolId = (int)TipoRol.SuperAdministrador,
                Email = Constants.EMAIL_SUPER_ADMIN_LESTOMA
            };
            var superadminDiego = new EUsuario
            {
                Id = 2,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion,
                Nombre = "Diego-Super",
                Apellido = "Lestoma-APP",
                Clave = hashSuperLestoma.Password,
                Salt = hashSuperLestoma.Salt,
                EstadoId = (int)TipoEstadoUsuario.Activado,
                RolId = (int)TipoRol.SuperAdministrador,
                Email = Constants.EMAIL_SUPER_ADMIN
            };

            var hashAdmin = HashHelper.Hash(Constants.PWD_ADMIN);
            var administrador = new EUsuario
            {
                Id = 3,
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

            var hashAuxiliar = HashHelper.Hash(Constants.PWD_AUXILIAR);
            var auxiliar1 = new EUsuario
            {
                Id = 4,
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
                Id = 5,
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
                Email = "tudec2020@gmail.com"
            };

            modelBuilder.Entity<EUsuario>()
                .HasData(new List<EUsuario>
                {
                    superadminDiego, superadminLestoma, administrador, auxiliar1, auxiliar2
                });

            var super = new ESuperAdministrador
            {
                Id = 1,
                UsuarioId = (short)superadminLestoma.Id
            };
            var super2 = new ESuperAdministrador
            {
                Id = 2,
                UsuarioId = (short)superadminDiego.Id
            };
            modelBuilder.Entity<ESuperAdministrador>()
             .HasData(new List<ESuperAdministrador>
             {
                    super,super2
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
                Descripcion = "queda ubicada en la universidad cundinamarca extensión faca",
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

            #region data protocolo
            var peerToPeerUpa1 = new EProtocoloCOM() { Id = 1, UpaId = upa1.Id, Nombre = EnumConfig.GetDescription(TipoComunicacion.P2P), Sigla = TipoComunicacion.P2P.ToString(), PrimerByteTrama = (byte)TipoComunicacion.P2P };
            var broadCastUpa1 = new EProtocoloCOM() { Id = 2, UpaId = upa1.Id, Nombre = EnumConfig.GetDescription(TipoComunicacion.P2MP), Sigla = TipoComunicacion.P2MP.ToString(), PrimerByteTrama = (byte)TipoComunicacion.P2MP };

            var peerToPeerUpa2 = new EProtocoloCOM() { Id = 3, UpaId = upa2.Id, Nombre = EnumConfig.GetDescription(TipoComunicacion.P2P), Sigla = TipoComunicacion.P2P.ToString(), PrimerByteTrama = (byte)TipoComunicacion.P2P };
            var broadCastUpa2 = new EProtocoloCOM() { Id = 4, UpaId = upa2.Id, Nombre = EnumConfig.GetDescription(TipoComunicacion.P2MP), Sigla = TipoComunicacion.P2MP.ToString(), PrimerByteTrama = (byte)TipoComunicacion.P2MP };

            modelBuilder.Entity<EProtocoloCOM>()
                .HasData(new List<EProtocoloCOM>
                {
                    peerToPeerUpa1,broadCastUpa1,peerToPeerUpa2,broadCastUpa2
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
            var detalle2UserAuxiliar = new EUpaActividad()
            {
                UpaId = upa1.Id,
                ActividadId = alimentacionPeces.Id,
                UsuarioId = auxiliar1.Id,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion
            };


            var detalle1UserAuxiliar2 = new EUpaActividad()
            {
                UpaId = upa2.Id,
                ActividadId = controlAgua.Id,
                UsuarioId = auxiliar2.Id,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion
            };
            var detalle2UserAuxiliar2 = new EUpaActividad()
            {
                UpaId = upa2.Id,
                ActividadId = alimentacionPeces.Id,
                UsuarioId = auxiliar2.Id,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion
            };

            modelBuilder.Entity<EUpaActividad>()
                .HasData(new List<EUpaActividad>
                {
                    detalle1_1,detalle1_2,detalle1UserAuxiliar,detalle2UserAuxiliar,detalle1UserAuxiliar2,detalle2UserAuxiliar2
                });
            #endregion

            #region data componentes

            ListadoEstadoComponente listadoEstadoComponente = new();
            var options = new JsonSerializerOptions { WriteIndented = true };
            List<string> listaJson = new();

            foreach (var item in listadoEstadoComponente.Listado)
            {
                listaJson.Add(JsonSerializer.Serialize(item, options));
            }

            var bombaDeOxigeno = new EComponenteLaboratorio()
            {
                Id = Guid.NewGuid(),
                NombreComponente = "BOMBA DE OXIGENO",
                ActividadId = alimentacionPeces.Id,
                JsonEstadoComponente = listaJson[0],
                UpaId = upa1.Id,
                ModuloComponenteId = actuador.Id,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion,
                DireccionRegistro = byte.MinValue,
            };

            var luzEstanque = new EComponenteLaboratorio()
            {
                Id = Guid.NewGuid(),
                NombreComponente = "LUZ ESTANQUE",
                ActividadId = alimentacionPeces.Id,
                JsonEstadoComponente = listaJson[0],
                UpaId = upa1.Id,
                ModuloComponenteId = actuador.Id,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion,
                DireccionRegistro = byte.MinValue + 1,
            };

            var ramdom2 = Reutilizables.RandomByteDireccionEsclavoAndRegistro();

            var dosificadorAlimento = new EComponenteLaboratorio()
            {
                Id = Guid.NewGuid(),
                NombreComponente = "DOSIFICADOR DE ALIMENTO",
                ActividadId = alimentacionPeces.Id,
                JsonEstadoComponente = listaJson[0],
                UpaId = upa1.Id,
                ModuloComponenteId = actuador.Id,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion,
                DireccionRegistro = byte.MinValue + 3,
            };

            var temperaturaH20 = new EComponenteLaboratorio()
            {
                Id = Guid.NewGuid(),
                NombreComponente = "TEMPERATURA H2O",
                ActividadId = controlAgua.Id,
                JsonEstadoComponente = listaJson[1],
                UpaId = upa1.Id,
                ModuloComponenteId = sensor.Id,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion,
                DireccionRegistro = byte.MinValue + 2,
            };

            var PH = new EComponenteLaboratorio()
            {
                Id = Guid.NewGuid(),
                NombreComponente = "PH",
                ActividadId = controlAgua.Id,
                JsonEstadoComponente = listaJson[1],
                UpaId = upa1.Id,
                ModuloComponenteId = sensor.Id,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion,
                DireccionRegistro = byte.MinValue,
            };

            var nivelTanque = new EComponenteLaboratorio()
            {
                Id = Guid.NewGuid(),
                NombreComponente = "NIVEL TANQUE",
                ActividadId = controlAgua.Id,
                JsonEstadoComponente = listaJson[1],
                UpaId = upa1.Id,
                ModuloComponenteId = sensor.Id,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion,
                DireccionRegistro = byte.MinValue + 1,
            };

            var ajustetemperaturaH20 = new EComponenteLaboratorio()
            {
                Id = Guid.NewGuid(),
                NombreComponente = "SP_TEMPERATURA H2O",
                ActividadId = controlAgua.Id,
                JsonEstadoComponente = listaJson[2],
                UpaId = upa1.Id,
                ModuloComponenteId = ajuste.Id,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion,
                DireccionRegistro = byte.MinValue + 2,
            };


            modelBuilder.Entity<EComponenteLaboratorio>()
                .HasData(new List<EComponenteLaboratorio>
                {
                    bombaDeOxigeno,luzEstanque,dosificadorAlimento,temperaturaH20,PH,
                    nivelTanque,ajustetemperaturaH20
                });
            #endregion

            #region data detalle laboratorio
            var detalle1 = new ELaboratorio()
            {
                Id = Guid.NewGuid(),
                ComponenteLaboratorioId = bombaDeOxigeno.Id,
                TramaRecibida = "49803CE33F8000008FC8",
                TramaEnviada = "6FDAF029000000009834",
                FechaCreacionServer = DateTime.Now,
                ValorCalculadoTramaRecibida = 1,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion,
                FechaCreacionDispositivo = DateTime.Now,
                EstadoInternet = true
            };
            var detalle2 = new ELaboratorio()
            {
                Id = Guid.NewGuid(),
                ComponenteLaboratorioId = luzEstanque.Id,
                TramaRecibida = "496D3C083F80000096D1",
                TramaEnviada = "495DF08E000000007B74",
                ValorCalculadoTramaRecibida = 1,
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion,
                FechaCreacionDispositivo = DateTime.Now,
                EstadoInternet = true
            };

            var detalle3 = new ELaboratorio()
            {
                Id = Guid.NewGuid(),
                ComponenteLaboratorioId = PH.Id,
                TramaRecibida = "6FB2F0DC410E66663E8F",
                ValorCalculadoTramaRecibida = 8.9,
                TramaEnviada = "493E0FA6000000007453",
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion,
                FechaCreacionDispositivo = DateTime.Now,
                EstadoInternet = true
            };

            var detalle4 = new ELaboratorio()
            {
                Id = Guid.NewGuid(),
                ComponenteLaboratorioId = PH.Id,
                TramaRecibida = "6FEFF08440D66666F1A3",
                ValorCalculadoTramaRecibida = 6.7,
                TramaEnviada = "493E0FA6000000007453",
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion,
                FechaCreacionDispositivo = DateTime.Now,
                EstadoInternet = true
            };

            var detalle5 = new ELaboratorio()
            {
                Id = Guid.NewGuid(),
                ComponenteLaboratorioId = ajustetemperaturaH20.Id,
                TramaRecibida = "6FEEF0D8434800001CA9",
                ValorCalculadoTramaRecibida = 200,
                ValorCalculadoTramaEnviada = 24,
                TramaEnviada = "49F2F04541C00000A19A",
                FechaCreacionServer = DateTime.Now,
                Ip = ip,
                Session = usersession,
                TipoDeAplicacion = aplicacion,
                FechaCreacionDispositivo = DateTime.Now,
                EstadoInternet = true
            };

            modelBuilder.Entity<ELaboratorio>()
                .HasData(new List<ELaboratorio>
                {
                    detalle1, detalle2, detalle3, detalle4, detalle5
                });
            #endregion
        }
        #endregion
    }
}
