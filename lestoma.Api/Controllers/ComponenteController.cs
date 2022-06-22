﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly IComponenteService _componentService;
        public ComponenteController(IMapper mapper, IComponenteService componenteService)
         : base(mapper)
        {
            _componentService = componenteService;

        }
        [HttpGet("paginar")]
        public async Task<IActionResult> GetCompPag([FromQuery] Paginacion pag)
        {
            var query = _componentService.GetAllAsQueryable();
            var list = await GetPaginacion<EComponenteLaboratorio, ComponentesDTO>(pag, query);
            var paginador = Paginador<ComponentesDTO>.CrearPaginador(query.Count(), list, pag);
            return Ok(pag);
        }
        [HttpGet("listado")]
        public async Task<IActionResult> GetComponente()
        {
            var query = await _componentService.GetAllAsync();
            var comp = Mapear<List<EComponenteLaboratorio>, List<ComponentesDTO>>(query.ToList());
            return Ok(comp);
        }
        [HttpGet("listado-nombres")]
        public IActionResult GetComponentesNombres()
        {
            var query = _componentService.GetComponentesJustNames();
            return Ok(query);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComponente(Guid id)
        {
            var response = await _componentService.GetByIdAsync(id);
            var compDTOSalida = Mapear<EComponenteLaboratorio, CrearComponenteRequest>((EComponenteLaboratorio)response.Data);
            response.Data = compDTOSalida;
            return Ok(response);
        }

        [HttpPost("crear")]

        public async Task<IActionResult> CrearComponente(CrearComponenteRequest comp)
        {
            var compDTO = Mapear<CrearComponenteRequest, EComponenteLaboratorio>(comp);
            var response = await _componentService.CrearAsync(compDTO);
            return Ok(response);
        }
        [HttpPut("editar")]

        public async Task<IActionResult> EditarComponente(CrearComponenteRequest comp)
        {
            var compDTO = Mapear<CrearComponenteRequest, EComponenteLaboratorio>(comp);
            var response = await _componentService.ActualizarAsync(compDTO);
            var comDTOSalida = Mapear<EComponenteLaboratorio, CrearComponenteRequest>((EComponenteLaboratorio)response.Data);
            response.Data = comDTOSalida;
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarComponent(Guid id)
        {
            await _componentService.EliminarAsync(id);
            return NoContent();
        }
    }


 }
