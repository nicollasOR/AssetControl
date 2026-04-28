using AssetControlAPI_.Contexts;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Interface;
using Microsoft.EntityFrameworkCore;

namespace AssetControlAPI_.Repository
{
    public class SolicitacaoTransferenciaRepository : ISolicitacaoTransferenciaRepository
    {

        private readonly AssetDb_Context _context;
        public SolicitacaoTransferenciaRepository(AssetDb_Context context) => _context = context;

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

        public Patrimonio BuscarPatrimonioPorId(Guid patrimonioId)
        {
            return _context.Patrimonio.Find(patrimonioId)!;
        }

        public void Atualizar(StatusTransferencia status)
        {
            if (status == null)
                return;
        }



        public void Adicionar(SolicitacaoTransferencia solicitacaoTransferencia)
        {
            _context.SolicitacaoTransferencia.Add(solicitacaoTransferencia);
            _context.SaveChanges();
        }

        public StatusPatrimonio BuscarStatusPatrimonioPorNome(string nomeStatus)
        {
            return _context.StatusPatrimonio.FirstOrDefault(status => status.NomeStatusPatrimonio.ToLower() == nomeStatus.ToLower())!;
        }

        public TipoAlteracao BuscarTipoAlteracaoPorNome(string nomeTipo)
        {
            return _context.TipoAlteracao.FirstOrDefault(tipo => tipo.NomeAlteracao.ToLower() == nomeTipo.ToLower())!;
        }

        public void Atualizar(SolicitacaoTransferencia solicitacaoTransferencia)
        {
            if (solicitacaoTransferencia == null)
            {
                return;
            }

            SolicitacaoTransferencia solicitacaoBanco = _context.SolicitacaoTransferencia.Find(solicitacaoTransferencia.StatusTransferenciaId)!;

            if (solicitacaoBanco == null)
            {
                return;
            }

            solicitacaoBanco.DataResposta = solicitacaoTransferencia.DataResposta;
            solicitacaoBanco.StatusTransferenciaId = solicitacaoTransferencia.StatusTransferenciaId;
            solicitacaoBanco.UsuarioAprovacaoId = solicitacaoTransferencia.UsuarioAprovacaoId;

            _context.SaveChanges();
        }

        public void AtualizarPatrimonio(Patrimonio patrimonio)
        {
            if (patrimonio == null)
            {
                return;
            }

            Patrimonio patrimonioBanco = _context.Patrimonio.Find(patrimonio.PatrimonioId)!;

            if (patrimonioBanco == null)
            {
                return;
            }

            patrimonioBanco.LocalizacaoId = patrimonio.LocalizacaoId;
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
