using AssetControlAPI_.Contexts;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Interface;
using Microsoft.EntityFrameworkCore;

namespace AssetControlAPI_.Repository
{
    public class LogPatrimonioRepository : ILogPatrimonioRepository
    {

        private readonly AssetDBContext _context;
        public LogPatrimonioRepository(AssetDBContext context) => _context = context;

        public List<LogPatrimonio> Listar()
        {

            return _context.LogPatrimonio
                .Include(logAux => logAux.Usuario)
                .Include(logAux => logAux.Patrimonio)
                .Include(logAux => logAux.Localizacao)
                .Include(logAux => logAux.StatusPatrimonio)
                .Include(logAux => logAux.TipoAlteracao)
                .OrderByDescending(logAux => logAux.DataTransferencia)
                .ToList();

        }

        public List<LogPatrimonio> BuscarPorPatrimonio(Guid patrimonioId)
        {
            return _context.LogPatrimonio
                 .Include(logAux => logAux.Usuario)
                 .Include(logAux => logAux.Patrimonio)
                 .Include(logAux => logAux.Localizacao)
                 .Include(logAux => logAux.StatusPatrimonio)
                 .Include(logAux => logAux.TipoAlteracao)
                 .Where(logAux => logAux.PatrimonioId == patrimonioId)
                 .OrderByDescending(logAux => logAux.DataTransferencia)
                 .ToList();
        }
    }
}
