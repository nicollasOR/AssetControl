namespace AssetControlAPI_.Applications.DTOs.SolicitacaoTransferenciaDTO
{
    public class CriarSolicitacaoTransferenciaDTO
    {
        public string Justificativa { get; set; } = string.Empty;
        public Guid patrimonioId { get; set; }
        public Guid localizacaoId { get; set; }


    }
}
