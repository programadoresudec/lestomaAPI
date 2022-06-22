using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace lestoma.Api.Controllers
{
    [Route("api/modulos")]
    [ApiController]

    public class ModuloController : BaseController
    {
        private readonly IModuloService _moduloService;

        public ModuloController(IMapper mapper IModuloService moduloService)
              : base(mapper)
        {
            _moduloService = moduloService;
        }
        
        [

        ]

    }
}