using AutoMapper;
using lestoma.Api.Helpers;
using lestoma.Data;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Api.Controllers
{
    [Authorize(Roles = RolesEstaticos.SUPERADMIN)]
    [Route("api/[controller]")]
    [ApiController]
    public class UpasActividadesController : BaseController
    {
        private readonly IUpasActividadesService _upasActividadesService;
        public UpasActividadesController(IMapper mapper,IUpasActividadesService upasActividadesService)
            : base(mapper)
        {
            _upasActividadesService = upasActividadesService;
        }

     
       
    }
}
