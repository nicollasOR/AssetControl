namespace AssetControlAPI_.Applications.DTOs.BairroDTO
{
    public class ListarBairroDTO
    {
        public Guid bairroId { get; set; } = Guid.Empty;
        public string bairroNome { get; set; } = string.Empty;
        public Guid cidadeId { get; set; } = Guid.Empty;
    }
}
