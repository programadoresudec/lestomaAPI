using AutoMapper;
using lestoma.CommonUtils.Constants;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Claims;
using System.Threading.Tasks;

namespace lestoma.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public readonly IMapper _mapper;
        public Response Respuesta { get; set; } = new Response();
        public BaseController(IMapper mapper)
        {
            _mapper = mapper;
        }

        #region GET Ip
        protected string ipAddress()
        {
            string IP4Address = String.Empty;

            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily == AddressFamily.InterNetwork)
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }
            return IP4Address;
        }
        #endregion

        #region Claims
        protected List<Claim> ClaimsToken()
        {
            List<Claim> claims = new List<Claim>();
            if (User.Identity.IsAuthenticated)
            {
                var identity = (ClaimsIdentity)User.Identity;
                claims = identity.Claims.ToList();
            }
            return claims;
        }

        protected int AplicacionId()
        {
            int sIdAplicacion = 0;
            var id = ClaimsToken().Where(x => x.Type == ClaimsConfig.ID_APLICACION).Select(c => c.Value).SingleOrDefault();
            if (int.TryParse(id, out int idAplicacion))
            {
                sIdAplicacion = idAplicacion;
            }
            return sIdAplicacion;
        }

        protected bool IsSuperAdmin()
        {
            bool IsSuperAdmin = false;
            int sIdRol = 0;
            var rolId = ClaimsToken().Where(x => x.Type == ClaimsConfig.ID_ROL).Select(c => c.Value).SingleOrDefault();
            if (int.TryParse(rolId, out int RolId))
            {
                sIdRol = RolId;
                IsSuperAdmin = sIdRol == (int)TipoRol.SuperAdministrador ? true : false;
            }


            return IsSuperAdmin;
        }
        #endregion

        #region Automapper generic
        protected TOutputEntity Mapear<TInputEntity, TOutputEntity>(TInputEntity InputEntity) where TOutputEntity : class
        {
            return _mapper.Map<TOutputEntity>(InputEntity);
        }
        #endregion

        #region Paginacion de listados
        protected async Task<List<TDTO>> GetPaginacion<TEntidad, TDTO>(Paginacion paginacionDTO,
           IQueryable<TEntidad> queryable)
           where TEntidad : class
        {
            var entidades = await queryable.Paginar(paginacionDTO).ToListAsync();
            return _mapper.Map<List<TDTO>>(entidades);
        }
        #endregion

    }
}
