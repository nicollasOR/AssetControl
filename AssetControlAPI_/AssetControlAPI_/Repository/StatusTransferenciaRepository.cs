using AssetControlAPI_.Contexts;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Interface;

namespace AssetControlAPI_.Repository
{
    public class StatusTransferenciaRepository : IStatusTransferenciaRepository
    {
        private readonly AssetDBContext _context;

        public StatusTransferenciaRepository(AssetDBContext context) => _context = context; 

        public List<StatusTransferencia> List()
        {
            return _context.StatusTransferencia.OrderBy(varAux => varAux.StatusTransferencia1).ToList();
        }

        public 

    }
}
