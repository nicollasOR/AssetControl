using AssetControlAPI_.Contexts;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Interface;

namespace AssetControlAPI_.Repository
{
    public class StatusPatrimonioRepository : IStatusPatrimonioRepository
    {

        private readonly AssetDb_Context _context;
        public StatusPatrimonioRepository(AssetDb_Context context) => _context = context;

        public List<StatusPatrimonio> Listar()
        {
            return _context.StatusPatrimonio.OrderBy(varAux => varAux.NomeStatusPatrimonio).ToList();
        }

        public StatusPatrimonio BuscarPorId(Guid statusPatrimonioId)
        {
            return _context.StatusPatrimonio.Find(statusPatrimonioId);
        }

        public StatusPatrimonio BuscarPorNome(string nomeStatus)
        {
            return _context.StatusPatrimonio.Find(nomeStatus);
        }

        public void Adicionar(StatusPatrimonio statusPatrimonio)
        {
            _context.StatusPatrimonio.Add(statusPatrimonio);
            _context.SaveChanges();
        }

        public void Atualizar(StatusPatrimonio statusPatrimonio)
        {
            if (statusPatrimonio == null)
                return;

            StatusPatrimonio? statusPatrimonioBanco = _context.StatusPatrimonio.Find(statusPatrimonio.StatusPatrimonioId);

            statusPatrimonioBanco.NomeStatusPatrimonio = statusPatrimonio.NomeStatusPatrimonio;

            _context.SaveChanges();
        }



    }
}
