
using lestoma.CommonUtils.Requests;
using lestoma.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Data.DAO
{
    public class DAOUsuario : GenericRepository<EUsuario>
    {
        private readonly Mapeo _db;
        public DAOUsuario(Mapeo db) 
            : base(db)
        {
            _db = db;
        }
        public async Task<EUsuario> Logeo(LoginRequest login)
        {

            return await _db.TablaUsuarios.Include(o => o.EstadoUsuario).
                Include(e => e.Rol).Where(x => x.Email.Equals(login.Email)).FirstOrDefaultAsync();
        }

        public async Task<EUsuario> GetByEmail(string email)
        {
            return await _db.TablaUsuarios.FirstOrDefaultAsync(x => x.Email.Equals(email));
        }

        public async Task<bool> ExisteCodigoVerificacion(string codigoRecuperacion)
        {
            return await _db.TablaUsuarios.AnyAsync(x => x.CodigoRecuperacion.Equals(codigoRecuperacion));
        }

        public async Task<EUsuario> UsuarioByCodigoVerificacion(string codigo) => 
            await _db.TablaUsuarios.Where(x => x.CodigoRecuperacion.Equals(codigo))
            .FirstOrDefaultAsync();

        public EUsuario UsuarioByToken(string token)
        {
            return _db.TablaUsuarios.Include(o => o.Rol).SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
        }

        public short ExpiracionToken(int aplicacionId) => 
            _db.TablaAplicaciones.FirstOrDefault(x => x.Id == aplicacionId).TiempoExpiracionToken;
    }
}
