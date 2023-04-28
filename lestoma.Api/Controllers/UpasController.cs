using AutoMapper;
using lestoma.Api.Core;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Requests;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Api.Controllers
{
    [Route("api/upas")]
    [ApiController]
    public class UpasController : BaseController
    {
        private readonly IUpaService _upaService;
        public UpasController(IMapper mapper, IDataProtectionProvider protectorProvider, IUpaService upaService)
            : base(mapper, protectorProvider)
        {
            _upaService = upaService;
        }

        [HttpGet("paginar")]
        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        public async Task<IActionResult> GetUpasPaginado([FromQuery] Paginacion paginacion)
        {
            var queryable = _upaService.GetAllForPagination();
            var listado = await GetPaginacion<EUpa, UpaDTO>(paginacion, queryable);
            var paginador = Paginador<UpaDTO>.CrearPaginador(queryable.Count(), listado, paginacion);
            return Ok(paginador);
        }
        [HttpGet("listado")]
        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        public async Task<IActionResult> GetUpas()
        {
            var query = await _upaService.GetAll();
            var upas = Mapear<List<EUpa>, List<UpaDTO>>(query.ToList());
            return Ok(upas);
        }


        [HttpGet("listar-nombres")]
        [AuthorizeRoles(TipoRol.SuperAdministrador, TipoRol.Administrador, TipoRol.Auxiliar)]
        public async Task<IActionResult> GetUpasNombres()
        {
            var query = await _upaService.GetUpasJustNames();
            return Ok(query);
        }

        [HttpGet("listar-nombres-protocolo/{upaId}")]
        [AuthorizeRoles(TipoRol.SuperAdministrador, TipoRol.Administrador, TipoRol.Auxiliar)]
        public async Task<IActionResult> GetProtcolosNombresPorUpaId(Guid upaId)
        {
            if (!IsSuperAdmin() && upaId == Guid.Empty)
            {
                upaId = UpaId();
            }
            var query = await _upaService.GetProtocolsByUpaId(upaId);
            return Ok(query);
        }

        [HttpGet("{id}")]
        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        public async Task<IActionResult> GetUpa(Guid id)
        {
            var response = await _upaService.GetById(id);
            var upaDTOSalida = Mapear<EUpa, UpaRequest>((EUpa)response.Data);
            response.Data = upaDTOSalida;
            return Ok(response);
        }

        [HttpPost("crear")]
        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        public async Task<IActionResult> CrearUpa(UpaRequest upa)
        {
            var idUser = UserIdDesencrypted();
            var upaDTO = Mapear<UpaRequest, EUpa>(upa);
            var idsuperAdmin = await _upaService.GetSuperAdminId(idUser);
            upaDTO.SuperAdminId = idsuperAdmin;
            var response = await _upaService.Create(upaDTO);
            var upaDTOSalida = Mapear<EUpa, UpaRequest>((EUpa)response.Data);
            response.Data = upaDTOSalida;
            return Created(string.Empty, response);
        }
        [HttpPut("editar")]
        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        public async Task<IActionResult> EditarUpa(UpaEditRequest upa)
        {
            var upaDTO = Mapear<UpaEditRequest, EUpa>(upa);
            var response = await _upaService.Update(upaDTO);
            var upaDTOSalida = Mapear<EUpa, UpaEditRequest>((EUpa)response.Data);
            response.Data = upaDTOSalida;
            return Ok(response);
        }
        [HttpPost("agregar-protocolo")]
        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        public async Task<IActionResult> CrearProtocolo(ProtocoloRequest request)
        {
            var protocolo = Mapear<ProtocoloRequest, EProtocoloCOM>(request);
            var response = await _upaService.CreateProtocol(protocolo);
            var upaDTOSalida = Mapear<EProtocoloCOM, ProtocoloRequest>((EProtocoloCOM)response.Data);
            response.Data = upaDTOSalida;
            return Created(string.Empty, response);
        }
        [HttpPut("editar-protocolo")]
        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        public async Task<IActionResult> EditarProtocolo(ProtocoloRequest request)
        {
            var protocolo = Mapear<ProtocoloRequest, EProtocoloCOM>(request);
            var response = await _upaService.UpdateProtocol(protocolo);
            var upaDTOSalida = Mapear<EProtocoloCOM, ProtocoloRequest>((EProtocoloCOM)response.Data);
            response.Data = upaDTOSalida;
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        public async Task<IActionResult> EliminarUpa(Guid id)
        {
            await _upaService.Delete(id);
            return NoContent();
        }

        [HttpDelete("eliminar-protocolo/{id}")]
        [AuthorizeRoles(TipoRol.SuperAdministrador)]
        public async Task<IActionResult> EliminarProtocolo(int id)
        {
            await _upaService.DeleteProtocol(id);
            return NoContent();
        }
    }
}
