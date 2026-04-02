namespace AssetControlAPI_.Applications.DTOs.StatusPatrimonio
{
    public class AtualizarStatusPatrimonio
    {
        public Guid StatusPatrimonioId { get; set; } = Guid.Empty;
        public string StatusPatrimonioNome { get; set; } = string.Empty;
    }
}
