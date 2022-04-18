using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lestoma.Logica.Interfaces;
using AutoMapper;
using lestoma.CommonUtils.Helpers;
using lestoma.Entidades.Models;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Requests;

namespace lestoma.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponenteController : BaseController
    {
        private readonly IComponenteService _componentS;
        public ComponenteController(IMapper mapper, IComponenteService componenteService)
         : base(mapper)
        {
            _componentS = componenteService;

        }
        [HttpGet("paginar")]
        public async Task<IActionResult> GetCompPag([FromQuery] Paginacion pag)
        {
            var query = _componentS.GetAllAsQueryable();
            var list = await GetPaginacion<EComponentesLaboratorio, ComponentesDTO>(pag, query);
            var paginador = Paginador<ComponentesDTO>.CrearPaginador(query.Count(), list, pag);
            return Ok(pag);
        }
        [HttpGet("listado")]
        public async Task<IActionResult> GetComponente()
        {
            var query = await _componentS.GetAllAsync();
            var comp = Mapear<List<EComponentesLaboratorio>, List<ComponentesDTO>>(query.ToList());
            return Ok(comp);
        }
        [HttpGet("listado-nombres")]
        public IActionResult GetComponentesNombres()
        {
            var query = _componentS.GetComponentesJustNames();
            return Ok(query);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompo(Guid id)
        {
            var response = await _componentS.GetByIdAsync(id);
            var compDTOSalida = Mapear<EComponentesLaboratorio, CrearComponenteRequest>((EComponentesLaboratorio)response.Data);
            response.Data = compDTOSalida;
            return Ok(response);

        }

        [HttpPost("crear")]

        public async Task<IActionResult> CrearComponente(CrearComponenteRequest comp)
        {
            var compDTO = Mapear<CrearComponenteRequest, EComponentesLaboratorio>(comp);
            var response = await _componentS.CrearAsync(compDTO);
            return Ok(response);
        }
        [HttpPut("editar")]

        public async Task<IActionResult> EditarComponente(CrearComponenteRequest comp)
        {
            var compDTO = Mapear<CrearComponenteRequest, EComponentesLaboratorio>(comp);
            var response = await _componentS.ActualizarAsync(compDTO);
            var comDTOSalida = Mapear<EComponentesLaboratorio, CrearComponenteRequest>((EComponentesLaboratorio)response.Data);
            response.Data = comDTOSalida;
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarComponent(Guid id)
        {
            await _componentS.EliminarAsync(id);
            return NoContent();
        }
    }


 }
