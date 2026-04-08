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
        //public SolicitacaoTransferencia BuscarStatusTransferenciaPorNome(string nome);
        

        public bool LocalizacaoExiste(Guid localizacaoId);

        public Patrimonio BuscarPatrimonio(Guid patrimonioId);
        public void Adicionar(SolicitacaoTransferencia solicitacaoTransferencia);
        public void Atualizar(SolicitacaoTransferencia solicitacaoTransferencia);
    }
}
