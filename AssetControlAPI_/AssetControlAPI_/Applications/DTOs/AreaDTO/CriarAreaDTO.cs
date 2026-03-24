using System.ComponentModel.DataAnnotations;

namespace AssetControlAPI_.Applications.DTOs.AreaDTO
{
    public class CriarAreaDTO
    {
        [Required(ErrorMessage = "O nome da área é obrigatório")]
        [StringLength(50, ErrorMessage = "A área precisa ter 50 caracteres")]
       public string NomeArea { get; set; } = string.Empty;

        //string.Empty = null proibido
        // string? = pode ser null
        //string = null!; ==> "nunca vai entrar null, foi o que ela disse"
    }
}
