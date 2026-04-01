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

        public Patrimonio BuscarPorNumeroPatrimonio(string numeroPatrimonio, Guid? patrimonioId = null)
        {
            return _context.Patrimonio.FirstOrDefault(varAux => varAux.NumeroSerie == numeroPatrimonio && varAux.PatrimonioId == patrimonioId);

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

            Patrimonio? patrimonioBanco = _context.Patrimonio.FirstOrDefault
                (varAux => varAux.PatrimonioId == patrimonio.PatrimonioId && varAux.StatusPatrimonioId == patrimonio.StatusPatrimonioId);
            if (patrimonioBanco == null)
               return;

            patrimonioBanco.PatrimonioId = patrimonio.PatrimonioId;
            patrimonioBanco.StatusPatrimonioId = patrimonio.StatusPatrimonioId;
        }

    }
}
