using AssetControlAPI_.Domains;

namespace AssetControlAPI_.Interface
{
    public interface IAreaRepository
    {

        public List<Area> Listar();

        public Area ObterPorGuid(Guid guid);

        public Area ObterPorNome(string areaNome);

        public void Adicionar(Area area);

        public void Atualizar(Area area);

    }
}
