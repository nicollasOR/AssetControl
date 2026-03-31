using AssetControlAPI_.Domains;

namespace AssetControlAPI_.Interface
{
    public interface ITipoUsuarioRepository
    {

        public List<TipoUsuario> Listar();

        public TipoUsuario ObterPorId(Guid id);
        public TipoUsuario ObterPorNome(string nome);
        public void Adicionar(TipoUsuario tipoUsuario);
        public void Atualizar(TipoUsuario tipoUsuario);



    }
}
