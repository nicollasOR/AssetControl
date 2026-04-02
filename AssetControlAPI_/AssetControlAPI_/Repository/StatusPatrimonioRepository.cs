using AssetControlAPI_.Contexts;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Interface;

namespace AssetControlAPI_.Repository
{
    public class StatusPatrimonioRepository : IStatusPatrimonioRepository
    {

        private readonly AssetDBContext _context;
        public StatusPatrimonioRepository(AssetDBContext context) => _context = context;

        public List<StatusPatrimonio> Listar()
        {
            return _context.StatusPatrimonio.OrderBy(varAux => varAux.StatusPatrimonio1).ToList();  
        }

        public StatusPatrimonio BuscarPorid(Guid id)
        {
            return _context.StatusPatrimonio.Find(id);
        }

        public StatusPatrimonio BuscarPorNome(string nome)
        {
            return _context.StatusPatrimonio.Find(nome);
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

            statusPatrimonioBanco.StatusPatrimonio1 = statusPatrimonio.StatusPatrimonio1;

            _context.SaveChanges();
        }



    }
}
