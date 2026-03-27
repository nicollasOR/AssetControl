using AssetControlAPI_.Domains;

namespace AssetControlAPI_.Interface
{
    public interface ICidadeRepository
    {

        public List<Cidade> Listar();
        public Cidade ObterPorId(Guid cidadeId);
        public Cidade ObterPorNomeEstado(string nomeCidade, string nomeEstado);
        public void Adicionar(Cidade cidade);
        public void Atualizar(Cidade cidade);



    }
}
