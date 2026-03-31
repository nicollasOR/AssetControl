using AssetControlAPI_.Contexts;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Interface;

namespace AssetControlAPI_.Repository
{
    public class TipoPatrimonioRepository : ITipoPatrimonioRepository
    {
        private readonly AssetDBContext _context;
        public TipoPatrimonioRepository(AssetDBContext context) => _context = context;

        public List<TipoPatrimonio> Listar()
        {
            return _context.TipoPatrimonio.OrderBy(varAux => varAux.NomeTipo).ToList();
        }

        public TipoPatrimonio ObterPorId(Guid tipoPatrimonioId)
        {
            return _context.TipoPatrimonio.Find(tipoPatrimonioId);
        }

        public TipoPatrimonio ObterPorNome(string nome)
        {
            return _context.TipoPatrimonio.FirstOrDefault(nomeAux => nomeAux.NomeTipo.ToLower() == nome.ToLower());

        }

        public void Adicionar(TipoPatrimonio tipoPatrimonio)
        {
            _context.TipoPatrimonio.Add(tipoPatrimonio);
            _context.SaveChanges();
        }

        public void Atualizar(TipoPatrimonio tipoPatrimonio)
        {
            if (tipoPatrimonio == null)
                return;

            TipoPatrimonio tipoPatrimonioBanco = _context.TipoPatrimonio.Find(tipoPatrimonio.TipoPatrimonioId);
            tipoPatrimonioBanco.NomeTipo = tipoPatrimonio.NomeTipo;

            _context.SaveChanges();
            
        }


    }
}
