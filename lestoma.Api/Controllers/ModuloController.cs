using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace lestoma.Api.Controllers
{
    [Route("api/modulos")]
    [ApiController]

    public class ModuloController : BaseController
    {
        private readonly IModuloService _moduloService;

        public ModuloController(IMapper mapper, IModuloService moduloService)
              : base(mapper)
        {
            _moduloService = moduloService;
        }
        

    }
}