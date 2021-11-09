using AutoMapper;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Helpers;
using lestoma.CRC.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculosCRCController : ControllerBase
    {
        [HttpPost("CRCModbus")]
        public IActionResult CalcularCRCModbus(string hexadecimal)
        {
            var crc = CalcularCRCHelper.CalculateCrc16Modbus(hexadecimal);
            
            var resultado = new List<byte>();
            resultado.Add(crc.ElementAt(1));
            resultado.Add(crc.ElementAt(0));

            string hexaCRC = Reutilizables.ByteArrayToHexString(resultado.ToArray());
            var response = new Response
            {
                Data = hexaCRC,
                Mensaje = "crc"
            };
            return Created(string.Empty, response);
        }
    }
}
