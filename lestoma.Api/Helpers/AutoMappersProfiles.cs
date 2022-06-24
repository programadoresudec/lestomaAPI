using AutoMapper;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Requests;
using lestoma.Entidades.Models;
using System.Text.Json;

namespace lestoma.Api.Helpers
{
    public class AutoMappersProfiles : Profile
    {
        public AutoMappersProfiles()
        {
            #region Request a entidad
            CreateMap<UsuarioRequest, EUsuario>().ReverseMap();
            CreateMap<UpaRequest, EUpa>().ReverseMap();
            CreateMap<RegistroRequest, EUsuario>();
            CreateMap<ActividadRequest, EActividad>().ReverseMap();
            CreateMap<ModuloRequest, EModuloComponente>().ReverseMap();
            CreateMap<CrearDetalleUpaActividadRequest, EUpaActividad>();
            CreateMap<CrearComponenteRequest, EComponenteLaboratorio>().ReverseMap();
            #endregion

            #region DTO a entidad 
            CreateMap<EUpa, UpaDTO>();
            CreateMap<EModuloComponente, ModuloDTO>();
            CreateMap<EComponenteLaboratorio, ComponentesDTO>();
            CreateMap<EActividad, ActividadDTO>();
            CreateMap<EUpaActividad, DetalleUpaActividadDTO>().ForMember(d => d.User, o => o.MapFrom(s => s.Usuario)).
                ForMember(u => u.Upa, x => x.MapFrom(s => s.Upa));
            CreateMap<EBuzon, BuzonDTO>().ForMember(d => d.Detalle, o => o.MapFrom(s => deserializarDetalleBuzon(s.Descripcion)));
            CreateMap<EUsuario, UserDTO>().ForMember(d => d.RolId, o => o.MapFrom(s => s.Rol.Id));
            CreateMap<EUsuario, InfoUserDTO>(); 
            #endregion
        }


        private DetalleBuzon deserializarDetalleBuzon(string descripcion)
        {
            return JsonSerializer.Deserialize<DetalleBuzon>(descripcion);
        }
    }
}
