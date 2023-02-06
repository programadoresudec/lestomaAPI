using AutoMapper;
using lestoma.Api.Core;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
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
    [Route("api/componentes")]
    [ApiController]
    public class ComponentsController : BaseController
    {
        private readonly IComponenteService _componentService;
        public ComponentsController(IMapper mapper, IComponenteService componenteService)
            : base(mapper)
        {
            _componentService = componenteService;

        }
        [HttpGet("paginar")]
        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        public async Task<IActionResult> GetAllFilter([FromQuery] ComponentFilterRequest filtro)
        {
            if (filtro.UpaId == Guid.Empty)
            {
                var upaId = UpaId();
                if (upaId != Guid.Empty)
                {
                    filtro.UpaId = upaId;
                }
            } 
            var queryable = _componentService.GetAllFilter(filtro.UpaId);
            var listado = await queryable.Paginar(filtro.Paginacion).ToListAsync();
            var paginador = Paginador<ListadoComponenteDTO>.CrearPaginador(queryable.Count(), listado, filtro.Paginacion);
            return Ok(paginador);
        }


        [HttpGet("listar-nombres")]
        [AuthorizeRoles(TipoRol.SuperAdministrador, TipoRol.Administrador, TipoRol.Auxiliar)]
        public async Task<IActionResult> GetComponentesNombres()
        {
            var query = await _componentService.GetComponentesJustNames();
            return Ok(query);
        }
        [HttpGet("{id}")]
        [AuthorizeRoles(TipoRol.SuperAdministrador, TipoRol.Administrador)]
        public async Task<IActionResult> GetComponente(Guid id)
        {
            var response = await _componentService.GetById(id);
            return Ok(response);
        }
        
        [HttpPost("crear")]
        [AuthorizeRoles(TipoRol.SuperAdministrador, TipoRol.Administrador)]
        public async Task<IActionResult> CrearComponente([FromBody] CreateComponenteRequest comp)
        {
            var compDTO = Mapear<CreateComponenteRequest, EComponenteLaboratorio>(comp);
            var response = await _componentService.Create(compDTO);
            return Created(string.Empty, response);
        }

        [HttpPut("editar")]
        [AuthorizeRoles(TipoRol.SuperAdministrador, TipoRol.Administrador)]
        public async Task<IActionResult> EditarComponente([FromBody] EditComponenteRequest comp)
        {
            var compDTO = Mapear<EditComponenteRequest, EComponenteLaboratorio>(comp);
            var response = await _componentService.Update(compDTO);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        [AuthorizeRoles(TipoRol.SuperAdministrador, TipoRol.Administrador)]
        public async Task<IActionResult> EliminarComponente(Guid id)
        {
            await _componentService.Delete(id);
            return NoContent();
        }
    }
}
