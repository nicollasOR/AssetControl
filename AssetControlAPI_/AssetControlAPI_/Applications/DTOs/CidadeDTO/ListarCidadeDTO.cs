namespace AssetControlAPI_.Applications.DTOs.CidadeDTO
{
    public class ListarCidadeDTO
    {
        Guid cidadeId { get; set; } = Guid.Empty;

        string cidadeNome { get; set; } = string.Empty;
        string estadoNome { get; set; } = string.Empty;
    }
}
