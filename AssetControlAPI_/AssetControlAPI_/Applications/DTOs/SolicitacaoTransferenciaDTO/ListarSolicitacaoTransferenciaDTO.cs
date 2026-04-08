namespace AssetControlAPI_.Applications.DTOs.SolicitacaoTransferenciaDTO
{
    public class ListarSolicitacaoTransferenciaDTO
    {
        public Guid Transferenciaid { get; set; }
        public Guid SolicitacaoTransferenciaId {get;set;} 
        public Guid UsuarioIdSolicitacao { get;set;}
        public Guid? UsuarioIdAprovacao { get; set; }
        public Guid localizacaoId { get; set; }
        public Guid patrimonioId { get; set; }
        public DateTime DataCriacaoSolicitante { get; set; }
        public DateTime? DataResposta { get; set; }
        public string Justificativa { get; set; } = string.Empty;
        
    }
}
