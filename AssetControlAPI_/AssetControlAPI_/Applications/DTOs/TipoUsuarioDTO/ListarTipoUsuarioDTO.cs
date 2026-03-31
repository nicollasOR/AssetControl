namespace AssetControlAPI_.Applications.DTOs.TipoUsuarioDTO
{
    public class ListarTipoUsuarioDTO
    {
        public Guid TipoUsuarioId { get; set; } = Guid.Empty;
        public string nomeTipoUsuario { get; set; } = null!; 

    }
}
