using lestoma.CommonUtils.Enums;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace lestoma.Api.Core
{
    /// <summary>
    /// Clase para autorizar varios roles en los controladores
    /// </summary>
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// se concatena los roles por comas
        /// </summary>
        /// <param name="rolesEnum"></param>
        public AuthorizeRolesAttribute(params TipoRol[] rolesEnum) : base()
        {
            List<string> roles = new List<string>();
            foreach (var item in rolesEnum)
            {
                string rol = EnumConfig.GetDescription(item);
                roles.Add(rol);
            }
            Roles = string.Join(",", roles);
        }
    }
}
