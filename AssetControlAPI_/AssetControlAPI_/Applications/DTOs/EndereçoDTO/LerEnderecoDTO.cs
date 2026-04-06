namespace AssetControlAPI_.Applications.DTOs.EndereçoDTO
{
    public class LerEnderecoDTO
    {

        public Guid enderecoId { get; set; } 
        public string Logradoura { get; set; } 
        public string CEP { get; set; } 

        public string Complemento { get; set; } 
        public Guid bairroId { get; set; }  

    }
}
