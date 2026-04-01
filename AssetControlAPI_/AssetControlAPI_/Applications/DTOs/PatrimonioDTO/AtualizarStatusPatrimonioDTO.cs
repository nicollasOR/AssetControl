namespace AssetControlAPI_.Applications.DTOs.PatrimonioDTO
{
    public class AtualizarStatusPatrimonioDTO
    {
        public Guid PatrimonioId { get; set; } = Guid.Empty;
        public Guid StatusPatrimonioId { get; set; } = Guid.Empty;
        public string NumeroSerie { get; set; } = string.Empty;


        public string Denominacao { get; set; } = string.Empty;

    }
}
