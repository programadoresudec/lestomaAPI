﻿using AutoMapper;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Requests;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Api.Controllers
{
    //[Authorize(Roles = RolesEstaticos.SUPERADMIN)]
    [Route("api/actividades")]
    [ApiController]
    public class ActividadController : BaseController
    {
        private readonly IActividadService _actividadService;
        public ActividadController(IMapper mapper, IActividadService actividadService)
            : base(mapper)
        {
            _actividadService = actividadService;
        }

        [HttpGet("paginar")]
        public async Task<IActionResult> GetActividadesPaginado([FromQuery] Paginacion paginacion)
        {
            var queryable = _actividadService.GetAllForPagination();
            var listado = await GetPaginacion<EActividad, ActividadDTO>(paginacion, queryable);
            var paginador = Paginador<ActividadDTO>.CrearPaginador(queryable.Count(), listado, paginacion);
            return Ok(paginador);
        }

        [HttpGet("listado")]
        public async Task<IActionResult> ListaActividades()
        {
            var query = await _actividadService.GetAll();
            var actividades = Mapear<IEnumerable<EActividad>, IEnumerable<ActividadDTO>>(query);
            return Ok(actividades);
        }

        [HttpGet("listado-nombres")]
        public IActionResult GetActividadesNombres()
        {
            var query = _actividadService.GetActividadesJustNames();
            return Ok(query);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> getActividad(Guid id)
        {
            var response = await _actividadService.GetById(id);
            var actividadDTOSalida = Mapear<EActividad, ActividadRequest>((EActividad)response.Data);
            response.Data = actividadDTOSalida;
            return Ok(response);
        }
        [HttpPost("crear")]
        public async Task<IActionResult> Crear(ActividadRequest actividad)
        {
            var objeto = Mapear<ActividadRequest, EActividad>(actividad);
            var response = await _actividadService.Create(objeto);
            return Created(string.Empty, response);
        }

        [HttpPut("editar")]
        public async Task<IActionResult> Editar(ActividadRequest actividad)
        {
            var objeto = Mapear<ActividadRequest, EActividad>(actividad);
            var response = await _actividadService.Update(objeto);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            await _actividadService.Delete(id);
            return NoContent();
        }

    }
}
