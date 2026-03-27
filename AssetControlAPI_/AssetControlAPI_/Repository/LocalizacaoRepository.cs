using AssetControlAPI_.Contexts;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Interface;

namespace AssetControlAPI_.Repository
{
    public class LocalizacaoRepository : ILocalizacaoRepository
    {

        private readonly AssetDBContext _context;
        public LocalizacaoRepository(AssetDBContext context) => _context = context;

        public List<Localizacao> Listar()
        {
            return _context
                .Localizacao.OrderBy(varAux => varAux.NomeLocalizacao).ToList();
        }

        public Localizacao ObterPorGuid(Guid guid)
        {
            return _context.Localizacao.Find(guid);
        }

        public Localizacao ObterPorNome(string nome, Guid areaId)
        {
             return _context.Localizacao.FirstOrDefault
                (
                localAux => localAux.NomeLocalizacao.ToLower() == nome.ToLower() 
                && localAux.AreaId == areaId);
        }

        public bool AreaExiste(Guid areaId)
        {
            return _context.Area.Any(area => area.AreaId == areaId);
            
        }

        public void Adicionar(Localizacao localizacao)
        {
            

            _context.Localizacao.Add(localizacao);
            _context.SaveChanges();
        }


        public void Atualizar(Localizacao localizacao)
        {

            if (localizacao == null)
                return;
            Localizacao localizacaoBanco = _context.Localizacao.Find(localizacao.LocalizacaoId);
            
            if (localizacaoBanco == null)
                return;

            localizacaoBanco.NomeLocalizacao = localizacao.NomeLocalizacao;
            localizacaoBanco.LocalizacaoId = localizacao.LocalizacaoId;
            localizacaoBanco.DescricaoSAP = localizacao.DescricaoSAP;
            localizacaoBanco.AreaId = localizacao.AreaId;


            _context.SaveChanges();
        }

    }
}
