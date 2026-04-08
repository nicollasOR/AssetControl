using AssetControlAPI_.Contexts;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Interface;
using Microsoft.EntityFrameworkCore;

namespace AssetControlAPI_.Repository
{
    public class SolicitacaoTransferenciaRepository : ISolicitacaoTransferenciaRepository
    {

        private readonly AssetDBContext _context;
        public SolicitacaoTransferenciaRepository(AssetDBContext context) => _context = context;

        public List<SolicitacaoTransferencia> Listar()
        {
            return _context.SolicitacaoTransferencia.OrderByDescending(varAux => varAux.DataSolicitacao).ToList();
        }

        public SolicitacaoTransferencia BuscarPorId(Guid id)
        {
            return _context.SolicitacaoTransferencia.Find(id);
        }

        public StatusTransferencia BuscarStatusTransferenciaPorNome(string nome)
        {
            return _context.StatusTransferencia.FirstOrDefault(varAux => varAux.StatusTransferencia1.ToLower() == nome.ToLower());
        }

        public bool ExisteSolicitacaoPendente(Guid patrimonioId)
        {

            StatusTransferencia statusPendente = BuscarStatusTransferenciaPorNome("Pendente de aprovação");

            if (statusPendente == null)
                return false;

            return _context.SolicitacaoTransferencia.Any(varAux => varAux.PatrimonioId == patrimonioId && varAux.StatusTransferenciaId == statusPendente.StatusTransferenciaId);
        }

        public bool UsuarioResponsavelDaLocalizacao(Guid usuarioid, Guid localizacaoId)
        {
            return _context.Usuario.Any(varAux => varAux.UsuarioId == usuarioid && varAux.Localizacao.Any(localizacao => localizacao.LocalizacaoId == localizacaoId));

        }

        public void Adicionar(StatusTransferencia status)
        {
            _context.StatusTransferencia.Add(status);
            _context.SaveChanges();
        }

        public bool LocalizacaoExiste(Guid localizacaoId)
        {
            return _context.Localizacao.Any(varAux => varAux.LocalizacaoId == localizacaoId);
        }

        public Patrimonio BuscarPorPatrimonio(Guid patrimonioId)
        {
            return _context.Patrimonio.Find(patrimonioId);
        }

        public void Atualizar(StatusTransferencia status)
        {
            if (status == null)
                return;


        }

    }
}
