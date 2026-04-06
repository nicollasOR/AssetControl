using AssetControlAPI_.Domains;

namespace AssetControlAPI_.Interface
{
    public interface IUsuarioRepository
    {
        public List<Usuario> Listar();

        public Usuario BuscarPorId(Guid id);
        public Usuario BuscarPorNIF(string NIF);
        public Usuario BuscarPorNome(string nome);

        public Usuario BuscarPor_ID_NIF_Nome(Guid? id, string NIF, string email);

        public Usuario BuscarPor_NIF_TipoUsuario(string nif);
        public bool UsuarioAtivo(bool Ativo);
        public bool NomeExiste(string nome);
        public bool nifExiste(string NIF);
        public bool enderecoExiste(Guid? enderecoId);
        public bool cargoExiste(Guid? cargoId);
        public bool tipoUsuarioExiste(Guid? tipoUsuarioId);
        public void Adicionar(Usuario usuario);
        public void Atualizar(Usuario usuario);
        public void AtualizarStatus(Usuario usuario);
        public void AtualizarSenha(Usuario usuario);
        public void AtualizarPrimeiroAcesso(Usuario usuario);


    }
}
