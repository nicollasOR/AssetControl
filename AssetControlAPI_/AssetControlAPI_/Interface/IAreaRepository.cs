using AssetControlAPI_.Domains;

namespace AssetControlAPI_.Interface
{
    public interface IAreaRepository
    {

        public List<Area> Listar();

        public Area ObterPorGuid(Guid guid);
    }
}
