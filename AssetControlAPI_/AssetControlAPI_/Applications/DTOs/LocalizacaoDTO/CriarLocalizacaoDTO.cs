namespace AssetControlAPI_.Applications.DTOs.LocalizacaoDTO
{
    public class CriarLocalizacaoDTO
    {

       public string? nomeLocal { get; set; } 
        public string DescricaoSAP { get; set; }
        public int localSAP { get; set; }
        public Guid AreaId { get; set; }

    }
}
