using AssetControlAPI_.Domains;

namespace AssetControlAPI_.Interface
{
    public interface ILocalizacaoRepository
    {

        public List<Localizacao> Listar();
        public Localizacao ObterPorGuid(Guid guid);

        public Localizacao ObterPorNome(string nome);

        public bool AreaExiste(Guid areaId);

        public void Adicionar(Localizacao localizacao);
        public void Atualizar(Localizacao localizacao);
    }
}
