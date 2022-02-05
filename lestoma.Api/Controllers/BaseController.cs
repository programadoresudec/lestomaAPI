using AutoMapper;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public readonly IMapper _mapper;
        public Response Respuesta { get; set; } = new Response();
        public BaseController(IMapper mapper)
        {
            _mapper = mapper;
        }

        protected TEntidad Mapear<TCreacion, TEntidad>(TCreacion creacionDTO) where TEntidad : class
        {
            return _mapper.Map<TEntidad>(creacionDTO);
        }

        protected async Task<List<TDTO>> GetPaginacion<TEntidad, TDTO>(Paginacion paginacionDTO,
            IQueryable<TEntidad> queryable)
            where TEntidad : class
        {
            var entidades = await queryable.Paginar(paginacionDTO).ToListAsync();
            return _mapper.Map<List<TDTO>>(entidades);
        }

    }
}
