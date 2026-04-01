using AssetControlAPI_.Domains;

namespace AssetControlAPI_.Interface
{
    public interface IUsuarioRepository
    {
        public List<Usuario> Listar();

        public Usuario BuscarPorId(Guid id);
        public Usuario BuscarPorNIF(string NIF);
        public Usuario BuscarPorNome(string nome);

        public bool UsuarioAtivo(bit Ativo);
        public bool NomeExiste(string nome);
        public bool nifExiste(string NIF);
        public void Adicionar(Usuario usuario);
        public void Remover(Usuario usuario);


    }
}
