namespace AssetControlAPI_.Applications.DTOs.PatrimonioDTO
{
    public class CriarPatrimonioDTO
    {

        public string Denominacao { get; set; } = string.Empty;

        public decimal Valor { get; set; } = decimal.Zero;

        public string NumeroSerie { get; set; } = string.Empty;

        public string? Imagem { get; set; } = string.Empty;

        public Guid LocalizacaoId { get; set; } = Guid.Empty;

        public Guid TipoPatrimonioId { get; set; } = Guid.Empty;

        public Guid StatusPatrimonioId { get; set; } = Guid.Empty;

    }
}
