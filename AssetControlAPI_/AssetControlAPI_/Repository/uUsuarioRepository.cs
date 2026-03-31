using AssetControlAPI_.Contexts;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Interface;

namespace AssetControlAPI_.Repository
{
    public class uUsuarioRepository : IUsuarioRepository
    {

        private readonly AssetDBContext _context;
        public uUsuarioRepository(AssetDBContext context) => _context = context;

        public List<Usuario> Listar()
        {
            return _context.Usuario.OrderBy(varAux => varAux.Nome).ToList();
        }

    }
}
