namespace AssetControlAPI_.Applications.DTOs.EndereçoDTO
{
    public class LerEnderecoDTO
    {

        public Guid enderecoId { get; set; } = Guid.Empty;
        public string Logradoura { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;

        public string Complemento { get; set; } = string.Empty;
        public Guid bairroId { get; set; } = Guid.Empty; 

    }
}
