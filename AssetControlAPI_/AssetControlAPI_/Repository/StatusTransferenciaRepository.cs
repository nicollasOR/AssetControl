using AssetControlAPI_.Contexts;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Interface;

namespace AssetControlAPI_.Repository
{
    public class StatusTransferenciaRepository : IStatusTransferenciaRepository
    {
        private readonly AssetDb_Context _context;

        public StatusTransferenciaRepository(AssetDb_Context context) => _context = context;

        public List<StatusTransferencia> Listar()
        {
            return _context.StatusTransferencia.OrderBy(varAux => varAux.StatusTransferencia1).ToList();
        }

        public StatusTransferencia BuscarPorId(Guid statusTransferenciaId)
        {
            return _context.StatusTransferencia.Find(statusTransferenciaId);
        }
        public StatusTransferencia BuscarPorNome(string nomeStatus)
        {
            return _context.StatusTransferencia.Find(nomeStatus);

        }
        public void Adicionar(StatusTransferencia statusTransferencia)
        {
            _context.StatusTransferencia.Add(statusTransferencia);
            _context.SaveChanges();
        }
        public void Atualizar(StatusTransferencia statusTransferencia)
        {
            if (statusTransferencia == null)
                return;

            StatusTransferencia? statusTransferenciaBanco = _context.StatusTransferencia.Find(statusTransferencia.StatusTransferenciaId);

            statusTransferenciaBanco.StatusTransferencia1 = statusTransferencia.StatusTransferencia1;
            _context.SaveChanges();

        }
    }
}
