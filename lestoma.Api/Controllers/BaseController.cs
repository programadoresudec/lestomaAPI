using AutoMapper;
using lestoma.CommonUtils.Constants;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Helpers;
using Microsoft.AspNetCore.DataProtection;
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
        protected readonly IMapper _mapper;
        protected readonly IDataProtector _protector;
        public ResponseDTO Respuesta { get; set; } = new ResponseDTO();
        public BaseController(IMapper mapper)
        {
            _mapper = mapper;
        }
        public BaseController(IMapper mapper, IDataProtectionProvider protectorProvider)
        {
            _mapper = mapper;
            _protector = protectorProvider.CreateProtector(Constants.PROTECT_USER);
        }
        #region GET Ip
        protected string IpAddress()
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
            List<Claim> claims = new();
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
            var id = ClaimsToken().Where(x => x.Type == ClaimsConfig.APLICACION_ID).Select(c => c.Value).SingleOrDefault();
            if (int.TryParse(id, out int idAplicacion))
            {
                sIdAplicacion = idAplicacion;
            }
            return sIdAplicacion;
        }


        protected string EmailDesencrypted()
        {
            string sEmailDesencrypted = string.Empty;
            var email = ClaimsToken().Where(x => x.Type == ClaimTypes.Email).Select(c => c.Value).SingleOrDefault();
            if (!string.IsNullOrWhiteSpace(email))
            {
                sEmailDesencrypted = _protector.Unprotect(email);
            }
            return sEmailDesencrypted;
        }

        protected int UserIdDesencrypted()
        {
            int sIdDesencrypted = 0;
            var id = ClaimsToken().Where(x => x.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
            if (!string.IsNullOrWhiteSpace(id))
            {
                sIdDesencrypted = Convert.ToInt32(_protector.Unprotect(id));
            }
            return sIdDesencrypted;
        }

        protected Guid UpaId()
        {
            Guid sId = Guid.Empty;
            var id = ClaimsToken().Where(x => x.Type == ClaimsConfig.UPA_ID).Select(c => c.Value).SingleOrDefault();
            if (Guid.TryParse(id, out Guid idUpa))
            {
                sId = idUpa;
            }
            return sId;
        }

        protected IEnumerable<Guid> ActividadesIds()
        {
            List<Guid> sIds = new List<Guid>();
            var ids = ClaimsToken().Where(x => x.Type == ClaimsConfig.PERMISOS_ACTIVIDADES_ID).Select(c => c.Value).ToList();

            foreach (var item in ids)
            {
                if (Guid.TryParse(item, out Guid idActividad))
                {
                    sIds.Add(idActividad);
                }
            }

            return sIds;
        }

        protected bool IsSuperAdmin()
        {
            bool IsSuperAdmin = false;
            int sIdRol = 0;
            var rolId = ClaimsToken().Where(x => x.Type == ClaimsConfig.ROL_ID).Select(c => c.Value).SingleOrDefault();
            if (int.TryParse(rolId, out int RolId))
            {
                sIdRol = RolId;
                IsSuperAdmin = sIdRol == (int)TipoRol.SuperAdministrador;
            }
            return IsSuperAdmin;
        }

        protected bool IsAuxiliar()
        {
            bool IsAuxiliar = false;
            int sIdRol = 0;
            var rolId = ClaimsToken().Where(x => x.Type == ClaimsConfig.ROL_ID).Select(c => c.Value).SingleOrDefault();
            if (int.TryParse(rolId, out int RolId))
            {
                sIdRol = RolId;
                IsAuxiliar = sIdRol == (int)TipoRol.Auxiliar;
            }
            return IsAuxiliar;
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
