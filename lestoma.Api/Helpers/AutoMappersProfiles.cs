using AutoMapper;
using lestoma.CommonUtils.Entities;
using lestoma.CommonUtils.Requests;
using lestoma.CommonUtils.Responses;
using System.Text.Json;

namespace lestoma.Api.Helpers
{
    public class AutoMappersProfiles : Profile
    {
        public AutoMappersProfiles()
        {
            CreateMap<UsuarioRequest, EUsuario>();
            CreateMap<EBuzon, BuzonResponse>().ForMember(d => d.Detalle, o => o.MapFrom(s => deserializarDetalleBuzon(s.Descripcion)));
            CreateMap<EUsuario, UserResponse>().ForMember(d => d.RolId, o => o.MapFrom(s => s.Rol.Id));
        }

        private DetalleBuzon deserializarDetalleBuzon(string descripcion)
        {
            return JsonSerializer.Deserialize<DetalleBuzon>(descripcion);
        }
    }
}
