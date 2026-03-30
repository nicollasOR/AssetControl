using System.ComponentModel.DataAnnotations;

namespace AssetControlAPI_.Applications.DTOs.BairroDTO
{
    public class CriarBairroDTO
    {

        [Required(ErrorMessage = "O nome da área é obrigatório")]
        [StringLength(50, ErrorMessage = "A área precisa ter 50 caracteres")]
        public string nomeBairro { get; set; } = string.Empty;
        public Guid cidadeId { get; set; } 
        

    }
}
