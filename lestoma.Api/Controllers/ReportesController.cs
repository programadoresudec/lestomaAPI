using AutoMapper;
using lestoma.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : BaseController
    {
        private readonly LestomaContext _context;
        #region Constructor
        public ReportesController(IMapper mapper, LestomaContext context)
            : base(mapper)
        {
            _context = context;
        }

        #endregion


        [HttpGet("listado")]
        public async Task<IActionResult> ListaActividades()
        {
            var query = await _context.TablaActividades.ToListAsync();
            return Ok(query);
        }
    }
}
