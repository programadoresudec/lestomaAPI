using System.Security.Claims;

namespace lestoma.Api.Helpers
{
    public interface IClaimsTransformation
    {
        ClaimsIdentity TransformAsync(ClaimsPrincipal principal);
    }
}
