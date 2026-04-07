namespace AssetControlAPI_.Applications.DTOs.EndereçoDTO
{
    public class CriarEnderecoDTO
    {
        public string Logradoura { get; set; } = string.Empty;
        public string? CEP { get; set; } = string.Empty;
        public int numero { get; set; } 
        public string? Complemento { get; set; }
        public Guid bairroId { get; set; } = Guid.Empty;


    }
}
