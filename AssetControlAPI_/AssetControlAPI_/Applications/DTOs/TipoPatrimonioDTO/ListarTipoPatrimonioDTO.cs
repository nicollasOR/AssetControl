namespace AssetControlAPI_.Applications.DTOs.TipoPatrimonioDTO
{
    public class ListarTipoPatrimonioDTO
    {
        public Guid tipoPatrimonioID { get; set; } = Guid.Empty;

        public string nomeTipoPatrimonio { get; set; } = null!;
    }
}
