using AutoMapper;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Requests;
using lestoma.CommonUtils.Requests.Filters;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponenteController : BaseController
    {
        private readonly IComponenteService _componentService;
        public ComponenteController(IMapper mapper, IComponenteService componenteService)
            : base(mapper)
        {
            _componentService = componenteService;

        }
        [HttpGet("paginar")]
        public async Task<IActionResult> GetAllFilter([FromQuery] ComponentFilterRequest filtro)
        {
            var upaId = UpaId();
            if (upaId != Guid.Empty)
            {
                filtro.UpaId = upaId;
            }
            var queryable = _componentService.GetAllFilter(filtro.UpaId);
            var listado = await queryable.Paginar(filtro.Paginacion).ToListAsync();
            var paginador = Paginador<ListadoComponenteDTO>.CrearPaginador(queryable.Count(), listado, filtro.Paginacion);
            return Ok(paginador);
        }


        [HttpGet("list-by-nombre")]
        public IActionResult GetComponentesNombres()
        {
            var query = _componentService.GetComponentesJustNames();
            return Ok(query);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComponente(Guid id)
        {
            var response = await _componentService.GetById(id);
            var compDTOSalida = Mapear<EComponenteLaboratorio, CreateOrEditComponenteRequest>((EComponenteLaboratorio)response.Data);
            response.Data = compDTOSalida;
            return Ok(response);
        }

        [HttpPost("crear")]

        public async Task<IActionResult> CrearComponente(CreateOrEditComponenteRequest comp)
        {
            var compDTO = Mapear<CreateOrEditComponenteRequest, EComponenteLaboratorio>(comp);
            var response = await _componentService.Create(compDTO);
            return Ok(response);
        }
        [HttpPut("editar")]

        public async Task<IActionResult> EditarComponente(CreateOrEditComponenteRequest comp)
        {
            var compDTO = Mapear<CreateOrEditComponenteRequest, EComponenteLaboratorio>(comp);
            var response = await _componentService.Update(compDTO);
            var comDTOSalida = Mapear<EComponenteLaboratorio, CreateOrEditComponenteRequest>((EComponenteLaboratorio)response.Data);
            response.Data = comDTOSalida;
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarComponent(Guid id)
        {
            await _componentService.Delete(id);
            return NoContent();
        }
    }


}
