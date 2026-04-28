using AssetControlAPI_.Contexts;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Interface;

namespace AssetControlAPI_.Repository
{
    public class TipoAlteracaoRepository : ITipoAlteracaoRepository
    {
        private readonly AssetDb_Context _context;

        public TipoAlteracaoRepository(AssetDb_Context context) => _context = context;

        public List<TipoAlteracao> Listar()
        {
            return _context.TipoAlteracao.OrderBy(varAux => varAux.NomeAlteracao).ToList();
        }

        public TipoAlteracao BuscarPorId(Guid guid)
        {
            return _context.TipoAlteracao.Find(guid);
        }

        public TipoAlteracao BuscarPorNome(string nome)
        {
            return _context.TipoAlteracao.Find(nome);
        }

        public void Adicionar(TipoAlteracao alteracao)
        {
            _context.TipoAlteracao.Add(alteracao);
            _context.SaveChanges();
        }

        public void Atualizar(TipoAlteracao alteracao)
        {

            if (alteracao == null)
                return;

            TipoAlteracao alteracaoBanco = _context.TipoAlteracao.Find(alteracao.TipoAlteracaoId);

            alteracaoBanco.NomeAlteracao = alteracao.NomeAlteracao;

            _context.SaveChanges();
        }
    }
}