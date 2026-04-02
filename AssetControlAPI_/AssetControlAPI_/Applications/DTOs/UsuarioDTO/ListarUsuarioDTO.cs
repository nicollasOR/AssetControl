namespace AssetControlAPI_.Applications.DTOs.UsuarioDTO
{
    public class ListarUsuarioDTO
    {
        public Guid UsuarioId { get; set; } = Guid.Empty;

        public string Nome { get; set; } = string.Empty;

        public string? RG { get; set; }

        public string CPF { get; set; } = string.Empty;

        public string CarteiraTrabalho { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        //public byte[] Senha { get; set; } = null!;
        public string NIF { get; set; } = string.Empty;


        public bool? Ativo { get; set; }

        public Guid EnderecoId { get; set; }

        public Guid CargoId { get; set; }

        public Guid TipoUsuarioId { get; set; }

        public bool PrimeiroAcesso { get; set; }


    }
}
