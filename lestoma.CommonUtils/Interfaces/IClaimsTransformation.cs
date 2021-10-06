using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace lestoma.Api.Helpers
{
    public interface IClaimsTransformation
    {
        ClaimsIdentity TransformAsync(ClaimsPrincipal principal);
    }
}
