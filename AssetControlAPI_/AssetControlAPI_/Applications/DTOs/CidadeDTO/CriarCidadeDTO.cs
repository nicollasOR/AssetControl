using System.ComponentModel.DataAnnotations;

namespace AssetControlAPI_.Applications.DTOs.CidadeDTO
{
    public class CriarCidadeDTO
    {
        [Required(ErrorMessage = "O nome da cidade é obrigatório")]
        [StringLength(50, ErrorMessage = "A cidade precisa ter 50 caracteres")]
        string NomeCidade { get; set; } = string.Empty;
        string nomeEstado { get; set; } = string.Empty;

    }
}
