namespace AssetControlAPI_.Applications.DTOs.UsuarioDTO
{
    public class CriarUsuarioDTO
    {

        public string Nome { get; set; } = null!;

        public string? RG { get; set; }

        public string CPF { get; set; } = null!;

        public string CarteiraTrabalho { get; set; } = null!;

        public string Email { get; set; } = null!;

        public byte[] Senha { get; set; } = null!;
        public string NIF { get; set; } = string.Empty;



        public Guid EnderecoId { get; set; }

        public Guid CargoId { get; set; }

        public Guid TipoUsuarioId { get; set; }

        public bool PrimeiroAcesso { get; set; }


    }
}
