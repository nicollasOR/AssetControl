using System.ComponentModel.DataAnnotations;

namespace AssetControlAPI_.Applications.DTOs.CidadeDTO
{
    public class CriarCidadeDTO
    {
        //[Required(ErrorMessage = "O nome da cidade é obrigatório")]
        //[StringLength(50, ErrorMessage = "A cidade precisa ter 50 caracteres")]
       public string NomeCidade { get; set; } = string.Empty;
        public  string nomeEstado { get; set; } = string.Empty;

    }
}
