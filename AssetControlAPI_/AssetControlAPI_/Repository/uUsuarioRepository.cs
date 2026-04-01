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
      public Usuario BuscarPorNome(string nome)
      {
         return _context.Usuario.Find(nome);
      }
     public Usuario BuscarPorId(Guid id)
     {
        return _context.Usuario.Find(id);
     }

     public Usuario BuscarPorNIF(string NIF)
     {
        return _context.Usuario.Find(NIF);
     } 

     public bool UsuarioAtivo(bit Ativo)
     {
        return _context.Usuario.Any(varAux => varAux.Ativo == Ativo);
     }

     public bool NomeExiste(string nome)
     {
        return _context.Usuario.Any(varAux => varAux.Nome == nome);
     }

     public bool nifExiste(string NIF)
     {
        return _context.Usuario.Any(varAux => varAux.NIF = NIF);
     }
    public void Adicionar(Usuario usuario)
    {
        _context.Usuario.Add(usuario);
        _context.SaveChanges();
    }
     
    public void Atualizar(Usuario usuario)
    {
        if(usuario == null)
        return;

        // Usuario usuarioBanco = _context.Usuario.Find(usuario.UsuarioId);
        // usuarioBanco.Nome = usuario.Nome;
        // usuarioBanco.RG = usuario.RG;
        // usuarioBanco.CPF = usuario.CPF;
        // usuarioBanco.Email = usuario.Email;
        // usuarioBanco.CarteiraTrabalho = usuario.CarteiraTrabalho;
        // usuarioBanco.Senha = usuario.Senha;
        // usuarioBanco.Ativo = usuario.Ativo;
        // usuarioBanco.EnderecoId = usuario.EnderecoId;
        // usuarioBanco.CargoId = usuario.CargoId;
        // usuarioBanco.TipoUsuarioId = usuario.TipoUsuarioId;
        // usuarioBanco.PrimeiroAcesso = usuario.PrimeiroAcesso;
        // usuarioBanco.NIF = usuario.NIF;
        
        // _context.Usuario.Update(usuario);
        _context.SaveChanges();
    }

    }
}
