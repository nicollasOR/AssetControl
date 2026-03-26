namespace AssetControlAPI_.Applications.DTOs.LocalizacaoDTO
{
    public class ListarLocalizacaoDTO
    {

        public Guid LocalizacaoId { get; set; }

        public string nomeLocal { get; set; } = string.Empty;
        public int? localSAP { get; set; }
        public string DescricaoSAP { get; set; }
        public Guid AreaId { get; set; }

    }
}
