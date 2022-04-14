using AutoMapper;
using lestoma.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace lestoma.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaboratorioController : BaseController
    {
        private readonly LestomaContext _context;
        #region Constructor
        public LaboratorioController(IMapper mapper, LestomaContext context)
            : base(mapper)
        {
            _context = context;
        }

        #endregion

        [HttpGet("listado")]
        public async Task<IActionResult> GetDetalle()
        {
            var listado = await _context.TablaDetalleLaboratorio.Include(x => x.ComponenteLaboratorio).Include(x => x.TipoDeComunicacion).ToListAsync();
            return Ok(listado);
        }

    }
}
