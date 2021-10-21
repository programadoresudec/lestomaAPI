using AutoMapper;
using lestoma.CommonUtils.DTOs;
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
    public class CalculosCRCController : BaseController
    {
        public CalculosCRCController(IMapper mapper) : base(mapper)
        {

        }

        [HttpPost("CRCModbus")]
        public async Task<IActionResult> CalcularCRCModbus(string hexadecimal)
        {
            var crc = CalcularCRCHelper.CalculateCrc16Modbus(hexadecimal);
            var response = new Response
            {
                Data = crc,
                Mensaje = "crc"
            };
            //byte[] bytesMOdbus = CalcularCRCHelper.CalculateCrc16Modbus(Trama);

            //var resultado = new List<byte>();
            //resultado.Add(bytesMOdbus.ElementAt(1));
            //resultado.Add(bytesMOdbus.ElementAt(0));

            //string hexa = HexaToByteHelper.ByteArrayToHexString(resultado.ToArray());
            return Created(string.Empty, response);
        }
    }
}
