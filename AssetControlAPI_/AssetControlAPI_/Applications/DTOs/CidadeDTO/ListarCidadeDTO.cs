namespace AssetControlAPI_.Applications.DTOs.CidadeDTO
{
    public class ListarCidadeDTO
    {
        public Guid cidadeId { get; set; } = Guid.Empty;

        public string cidadeNome { get; set; } = string.Empty;
        public string estadoNome { get; set; } = string.Empty;
    }
}
