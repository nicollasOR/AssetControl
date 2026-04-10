using AssetControlAPI_.Domains;

namespace AssetControlAPI_.Interface
{
    public interface ISolicitacaoTransferenciaRepository
    {
        public List<SolicitacaoTransferencia> Listar();

        public SolicitacaoTransferencia BuscarPorId(Guid id);

        public bool ExisteSolicitacaoPendente(Guid patrimonioId);
        public bool UsuarioResponsavelDaLocalizacao(Guid usuarioId, Guid localizacaoid);

        public StatusTransferencia BuscarStatusTransferenciaPorNome(string nome);


        public bool LocalizacaoExiste(Guid localizacaoId);
        public void Adicionar(SolicitacaoTransferencia solicitacaoTransferencia);
        public void Atualizar(SolicitacaoTransferencia solicitacaoTransferencia);


        public Patrimonio BuscarPatrimonioPorId(Guid patrimonioId);
        public StatusPatrimonio BuscarStatusPatrimonioPorNome(string nomeStatus);
        public TipoAlteracao BuscarTipoAlteracaoPorNome(string nomeTipo);
        public void AtualizarPatrimonio(Patrimonio patrimonio);
        public void AdicionarLog(LogPatrimonio logPatrimonio);

    }
}
