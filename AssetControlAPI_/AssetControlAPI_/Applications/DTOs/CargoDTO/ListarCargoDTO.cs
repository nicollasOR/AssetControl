namespace AssetControlAPI_.Applications.DTOs.CargoDTO
{
    public class ListarCargoDTO
    {
        public Guid CargoId { get; set; } = Guid.Empty;
        public string CargoNome { get; set; } = string.Empty;
    }
}
