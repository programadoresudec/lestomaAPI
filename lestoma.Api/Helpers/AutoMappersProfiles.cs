using AutoMapper;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Requests;
using lestoma.Entidades.Models;
using System;

namespace lestoma.Api.Helpers
{
    public class AutoMappersProfiles : Profile
    {
        public AutoMappersProfiles()
        {
            #region Request a entidad
            CreateMap<UsuarioRequest, EUsuario>().ReverseMap();
            CreateMap<UpaRequest, EUpa>().ReverseMap();
            CreateMap<UpaEditRequest, EUpa>().ReverseMap();
            CreateMap<ProtocoloRequest, EProtocoloCOM>().ReverseMap();
            CreateMap<RegistroRequest, EUsuario>();
            CreateMap<RegistroUpdateRequest, EUsuario>().ForMember(d => d.Id, o => o.MapFrom(s => s.UsuarioId));
            CreateMap<ActividadRequest, EActividad>().ReverseMap();
            CreateMap<ModuloRequest, EModuloComponente>().ReverseMap();
            CreateMap<CrearDetalleUpaActividadRequest, EUpaActividad>();

            CreateMap<CreateComponenteRequest, EComponenteLaboratorio>()
                .ForMember(d => d.NombreComponente, o => o.MapFrom(s => s.Nombre));

            CreateMap<EditComponenteRequest, EComponenteLaboratorio>()
                .ForMember(d => d.NombreComponente, o => o.MapFrom(s => s.Nombre));

            CreateMap<LaboratorioRequest, ELaboratorio>()

               .ForMember(d => d.ComponenteLaboratorioId, o => o.MapFrom(s => s.ComponenteId))
               .ForMember(d => d.FechaCreacionDispositivo, o => o.MapFrom(s => s.FechaCreacionDispositivo == null ? DateTime.Now : s.FechaCreacionDispositivo.Value))
               .ForMember(d => d.ValorCalculadoTramaEnviada, o => o.MapFrom(s => s.SetPointIn))
               .ForMember(d => d.ValorCalculadoTramaRecibida, o => o.MapFrom(s => s.SetPointOut));
            #endregion

            #region DTO a entidad 

            CreateMap<EEstadoUsuario, EstadoDTO>()
             .ForMember(d => d.NombreEstado, o => o.MapFrom(s => s.DescripcionEstado));
            CreateMap<ERol, RolDTO>();
            CreateMap<EUsuario, InfoUserDTO>().ForMember(d => d.Rol, o => o.MapFrom(s => s.Rol))
              .ForMember(d => d.Estado, o => o.MapFrom(s => s.EstadoUsuario));
            CreateMap<EUpa, UpaDTO>();
            CreateMap<EModuloComponente, ModuloDTO>();
            CreateMap<EActividad, ActividadDTO>();
            CreateMap<EUpaActividad, DetalleUpaActividadDTO>();
            CreateMap<EUsuario, UserDTO>().ForMember(d => d.RolId, o => o.MapFrom(s => s.Rol.Id))
                .ForMember(d => d.NombreRol, o => o.MapFrom(s => s.Rol.NombreRol));
            #endregion
        }
    }
}
