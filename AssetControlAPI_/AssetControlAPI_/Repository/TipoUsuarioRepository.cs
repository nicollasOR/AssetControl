using AssetControlAPI_.Contexts;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Interface;

namespace AssetControlAPI_.Repository
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {

        private readonly AssetDBContext _context;
        public TipoUsuarioRepository(AssetDBContext context) => _context = context;

        public List<TipoUsuario> Listar()
        {
            return _context.TipoUsuario.OrderBy(varAux => varAux.Nome).ToList();
        }

        public TipoUsuario ObterPorId(Guid id)
        {
            return _context.TipoUsuario.Find(id);
        }

        public TipoUsuario ObterPorNome(string nome)
        {
            return _context.TipoUsuario.Find(nome);
        }

        public void Adicionar(TipoUsuario usuario)
        {
            _context.TipoUsuario.Add(usuario);
            _context.SaveChanges();
        }

        public void Atualizar(TipoUsuario usuario)
        {
            if (usuario == null)
                return;

            TipoUsuario usuarioBanco = _context.TipoUsuario.Find(usuario.TipoUsuarioId);
            usuarioBanco.Nome = usuario.Nome;
        }
    }
}
