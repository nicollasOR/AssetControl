namespace AssetControlAPI_.Applications.DTOs.EndereçoDTO
{
    public class CriarEnderecoDTO
    {
        public int numero { get; set; } 
        public string Logradoura { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
        public string Complemento {get;set;} = string.Empty;
        public Guid bairroId { get; set; }= Guid.Empty;
        

    }
}
