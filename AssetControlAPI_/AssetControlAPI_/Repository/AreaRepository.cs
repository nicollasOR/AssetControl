

using AssetControlAPI_.Contexts;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Interface;

namespace AssetControlAPI_.Repository
{
    public class AreaRepository : IAreaRepository

    {
        private readonly AssetDBContext _context;
        public AreaRepository(AssetDBContext context) => _context = context;

       public List<Area> Listar()
        {
            return _context.Area.OrderBy(area => area.NomeArea).ToList();
        }

       public Area ObterPorGuid(Guid guid)
        {
            
        }

    }
}
