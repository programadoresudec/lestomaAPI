using AutoMapper;
using lestoma.Api.Helpers;
using lestoma.CommonUtils.Requests;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lestoma.Api.Controllers
{
    [Authorize(Roles = RolesEstaticos.SUPERADMIN)]
    [Route("api/[controller]")]
    [ApiController]
    public class UpaController : BaseController
    {
        private readonly IUpaService _upaService;
        public UpaController(IMapper mapper, IUpaService upaService)
            : base(mapper)
        {
            _upaService = upaService;
        }
        [HttpGet("listado")]
        public async Task<IActionResult> GetUpas()
        {
            var query = await _upaService.ListaUpas();
            if (query.Count == 0)
            {
                return NoContent();
            }
            var upas = Mapear<List<EUpa>, List<UpaRequest>>(query);
            return Ok(upas);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUpa(int id)
        {
            var response = await _upaService.GetUpa(id);
            var upaDTOSalida = Mapear<EUpa, UpaRequest>((EUpa)response.Data);
            response.Data = upaDTOSalida;
            return Ok(response);
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearUpa(UpaRequest upa)
        {
            var upaDTO = Mapear<UpaRequest, EUpa>(upa);
            var response = await _upaService.CrearUpa(upaDTO);
            if (response.Data == null)
            {
                return Conflict(response);
            }
            var upaDTOSalida = Mapear<EUpa, UpaRequest>((EUpa)response.Data);
            response.Data = upaDTOSalida;
            return CreatedAtAction(nameof(GetUpa), new { id = ((EUpa)response.Data).Id }, response);
        }


    }
}
