using AssetControlAPI_.Contexts;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Interface;
using Microsoft.EntityFrameworkCore;

namespace AssetControlAPI_.Repository
{
    public class PatrimonioRepository : IPatrimonioRepository
    {

        private readonly AssetDBContext _context;
        public PatrimonioRepository(AssetDBContext context) => _context = context;

        public List<Patrimonio> Listar()
        {
            return _context.Patrimonio.OrderBy(varAux => varAux.Denominacao).ToList();
        }

        public Patrimonio BuscarPorId(Guid id)
        {
            return _context.Patrimonio.Find(id);
        }

        public Patrimonio BuscarPorNome(string numeroPatrimonio, Guid? patrimonioId = null)
        {
            var banco = _context.Patrimonio.FirstOrDefault(varAux => varAux.Denominacao == numeroPatrimonio && varAux.PatrimonioId == patrimonioId);
            return banco;
        }

        public bool BuscarPorNumeroPatrimonio(string numeroPatrimonio)
        {
            return _context.Patrimonio.Any
                (varAux => varAux.NumeroSerie == numeroPatrimonio);
        }
        public Localizacao BuscarPorLocalizacaoNome(string nomeLocalizacao)
        {
            return _context.Localizacao.FirstOrDefault(varAux => varAux.NomeLocalizacao.ToLower() == nomeLocalizacao.ToLower());
        }

        public StatusPatrimonio BuscarStatusPatrimonioPorNome(string nomeStatus)
        {
            return _context.StatusPatrimonio.FirstOrDefault(varAux => varAux.StatusPatrimonio1.ToLower() == nomeStatus.ToLower());
        }
        
        public TipoAlteracao BuscarTipoAlteracaoPorNome(string nomeTipo)
        {
            return _context.TipoAlteracao.FirstOrDefault(varAux => varAux.NomeAlteracao.ToLower() == nomeTipo.ToLower());
        }


        public bool LocalizacaoExiste(Guid localizacaoId)
        {
            return _context.Patrimonio.Any(varAux => varAux.LocalizacaoId == localizacaoId);
        }

        public bool StatusPatrimonioExiste(Guid statusPatrimonioId)
        {
            return _context.Patrimonio.Any(varAux => varAux.StatusPatrimonioId == statusPatrimonioId);
        }
        public bool TipoPatrimonioExiste(Guid tipoPatrimonioId)
        {
            return _context.Patrimonio.Any(varAux => varAux.TipoPatrimonioId == tipoPatrimonioId);
        }

        public void Adicionar(Patrimonio patrimonio)
        {
            _context.Patrimonio.Add(patrimonio);
            _context.SaveChanges();
        }

        public void Atualizar(Patrimonio patrimonio)
        {
            if (patrimonio == null)
                return;

            Patrimonio? patrimonioBanco = _context.Patrimonio.Find(patrimonio.PatrimonioId);
            if (patrimonioBanco == null)
                return;

            patrimonioBanco.LocalizacaoId = patrimonio.LocalizacaoId;
            patrimonioBanco.Valor = patrimonio.Valor;
            patrimonioBanco.Denominacao = patrimonio.Denominacao;
            patrimonioBanco.Imagem = patrimonio.Imagem;
            patrimonioBanco.NumeroSerie = patrimonio.NumeroSerie;
            patrimonioBanco.LogPatrimonio = patrimonio.LogPatrimonio;
            patrimonioBanco.StatusPatrimonioId = patrimonio.StatusPatrimonioId;
            patrimonioBanco.TipoPatrimonioId = patrimonio.TipoPatrimonioId;
        }

        public void AtualizarStatus(Patrimonio patrimonio)
        {
            if (patrimonio == null)
                return;

            Patrimonio patrimonioBanco = _context.Patrimonio.Find(patrimonio.PatrimonioId);

            if (patrimonioBanco == null)
                return;

            patrimonioBanco.StatusPatrimonioId = patrimonio.StatusPatrimonioId;

            _context.SaveChanges();
        }

        public void AdicionarLog(LogPatrimonio logPatrimonio)
        {
            _context.LogPatrimonio.Add(logPatrimonio);
            _context.SaveChanges();
        }

    }
}
