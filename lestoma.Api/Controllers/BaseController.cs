using AutoMapper;
using lestoma.CommonUtils.Responses;
using Microsoft.AspNetCore.Mvc;

namespace lestoma.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public readonly IMapper _mapper;
        public Response Respuesta { get; set; }
        public BaseController(IMapper mapper)
        {
            this._mapper = mapper;
        }

        protected TEntidad Mapear<TCreacion, TEntidad>(TCreacion creacionDTO) where TEntidad : class
        {
            return _mapper.Map<TEntidad>(creacionDTO);
        }
    }
}
