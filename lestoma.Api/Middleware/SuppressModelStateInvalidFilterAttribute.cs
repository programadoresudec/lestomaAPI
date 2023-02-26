using lestoma.CommonUtils.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace lestoma.Api.Middleware
{
    public static class SuppressModelStateInvalidFilterAttribute
    {
        public static IEnumerable<ErrorEntryDTO> ModelStateErrorsToString(this ModelStateDictionary modelState)
        {
            var query = from kvp in modelState
                        from e in kvp.Value.Errors
                        select new ErrorEntryDTO
                        {
                            Source = kvp.Key,
                            TitleError = e.ErrorMessage
                        };
            return query;
        }
    }
}
