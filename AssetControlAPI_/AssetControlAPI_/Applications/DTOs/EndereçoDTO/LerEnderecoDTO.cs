namespace AssetControlAPI_.Applications.DTOs.EndereçoDTO
{
    public class LerEnderecoDTO
    {

        public Guid enderecoId { get; set; } 
        public string Logradoura { get; set; }  = string.Empty;
        public string? CEP { get; set; } 
        public int? numero { get; set; }
        public string? Complemento { get; set; }
        public Guid bairroId { get; set; } 
    }
}
