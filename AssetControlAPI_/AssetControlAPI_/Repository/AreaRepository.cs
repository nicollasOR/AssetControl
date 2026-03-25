

using AssetControlAPI_.Contexts;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Interface;
using Microsoft.EntityFrameworkCore;

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
            return _context.Area.Find(guid)!;
       }
        
       public Area ObterPorNome(string nomeArea)
       {
            return _context.Area.FirstOrDefault(nomeAux => nomeAux.NomeArea.ToLower() == nomeArea.ToLower());
       }

       public void Adicionar(Area area)
        {
            _context.Area.Add(area);
            _context.SaveChanges();
        }

        public void Atualizar(Area area)
        {
            if (area == null)
                return;
            Area areaBanco = _context.Area.Find(area.AreaId);

            areaBanco.NomeArea = area.NomeArea;
            _context.SaveChanges();
        }

    
    }
}
