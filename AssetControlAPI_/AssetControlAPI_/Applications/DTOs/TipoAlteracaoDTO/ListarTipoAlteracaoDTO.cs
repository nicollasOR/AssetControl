namespace AssetControlAPI_.Applications.DTOs.TipoAlteracaoDTO
{
    public class ListarTipoAlteracaoDTO
    {
        public Guid TipoAlteracaoId { get; set; } = Guid.Empty;
        public string nome { get; set; } = string.Empty;
    }
}
