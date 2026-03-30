using AssetControlAPI_.Domains;

namespace AssetControlAPI_.Interface
{
    public interface IBairroRepository
    {

        public List<Bairro> Listar();
        public Bairro ObterPorGuid(Guid bairroId);
        public void Adicionar(Bairro bairro);
        public void Atualizar(Bairro bairro);

        public Bairro ObterPorNomeEstado(string nomeBairro, Guid cidadeId);
        public bool CidadeExiste(Guid bairroId);

    }
}
