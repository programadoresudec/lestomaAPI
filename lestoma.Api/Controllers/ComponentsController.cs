using AutoMapper;
using lestoma.Api.Core;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Requests;
using lestoma.CommonUtils.Requests.Filters;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using lestoma.Logica.LogicaService;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Api.Controllers
{
    [Route("api/componentes")]
    [ApiController]
    public class ComponentsController : BaseController
    {
        private readonly IComponenteService _componentService;
        private readonly IDetalleUpaActividadService _detalleUpaActividadService;
        public ComponentsController(IMapper mapper, IDataProtectionProvider protectorProvider,
            IComponenteService componenteService, IDetalleUpaActividadService detalleUpaActividadService)
            : base(mapper, protectorProvider)
        {
            _detalleUpaActividadService = detalleUpaActividadService;
            _componentService = componenteService;

        }
        [HttpGet("paginar")]
        [AuthorizeRoles(TipoRol.SuperAdministrador, TipoRol.Administrador)]
        public async Task<IActionResult> GetAllFilter([FromQuery] ComponentFilterRequest filtro)
        {
            IEnumerable<NameDTO> actividades = null;
            UpaActivitiesFilterRequest UpaActivitiesfilter = new();
            if (!IsSuperAdmin() && filtro.UpaId == Guid.Empty)
            {
                filtro.UpaId = UpaId();
                actividades = await _detalleUpaActividadService.GetActivitiesByUpaUserId(new UpaUserFilterRequest
                {
                    UpaId = filtro.UpaId,
                    UsuarioId = UserIdDesencrypted()
                });
                UpaActivitiesfilter.ActividadesId = actividades.Any() ? actividades.Select(x => x.Id).ToList() : null;
                UpaActivitiesfilter.UpaId = UpaId();
            }
            else
            {
                UpaActivitiesfilter.UpaId = filtro.UpaId;
            }

            var queryable = _componentService.GetAllFilter(UpaActivitiesfilter);
            var listado = await queryable.Paginar(filtro.Paginacion).ToListAsync();
            var paginador = Paginador<ListadoComponenteDTO>.CrearPaginador(queryable.Count(), listado, filtro.Paginacion);
            return Ok(paginador);
        }


        [HttpGet("listar-nombres")]
        [AuthorizeRoles(TipoRol.SuperAdministrador, TipoRol.Administrador, TipoRol.Auxiliar)]
        public async Task<IActionResult> GetComponentesNombres()
        {
            var query = await _componentService.GetComponentsJustNames();
            return Ok(query);
        }

        [HttpGet("listar-nombres-por-upa/{upaId}")]
        [AuthorizeRoles(TipoRol.SuperAdministrador, TipoRol.Administrador)]
        public async Task<IActionResult> GetComponentesByUpa(Guid upaId)
        {
            IEnumerable<NameDTO> actividades = null;
            UpaActivitiesFilterRequest UpaActivitiesfilter = new UpaActivitiesFilterRequest();
            if (!IsSuperAdmin())
            {
                actividades = await _detalleUpaActividadService.GetActivitiesByUpaUserId(new UpaUserFilterRequest
                {
                    UpaId = UpaId(),
                    UsuarioId = UserIdDesencrypted()
                });
                UpaActivitiesfilter.ActividadesId = actividades.Any() ? actividades.Select(x => x.Id).ToList() : null;
                UpaActivitiesfilter.UpaId = UpaId();
            }
            else
            {
                UpaActivitiesfilter.UpaId = upaId;
            }
            var query = await _componentService.GetComponentsJustNamesById(UpaActivitiesfilter, IsSuperAdmin());
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
            if (!IsSuperAdmin() && comp.UpaId == Guid.Empty)
            {
                comp.UpaId = UpaId();
            }
            var compDTO = Mapear<CreateComponenteRequest, EComponenteLaboratorio>(comp);
            var response = await _componentService.Create(compDTO);
            return Created(string.Empty, response);
        }

        [HttpPut("editar")]
        [AuthorizeRoles(TipoRol.SuperAdministrador, TipoRol.Administrador)]
        public async Task<IActionResult> EditarComponente([FromBody] EditComponenteRequest comp)
        {
            if (!IsSuperAdmin() && comp.UpaId == Guid.Empty)
            {
                comp.UpaId = UpaId();
            }
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
