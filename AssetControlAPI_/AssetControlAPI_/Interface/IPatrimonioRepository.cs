using AssetControlAPI_.Domains;

namespace AssetControlAPI_.Interface
{
    public interface IPatrimonioRepository
    {

        public List<Patrimonio> Listar();
        public Patrimonio BuscarPorId(Guid patrimonioId);
        public Patrimonio BuscarPorNumeroPatrimonio(string numeroPatrimonio, Guid? patrimonioId = null);
        public bool LocalizacaoExiste(Guid localizacaoId);
        public bool TipoPatrimonioExiste(Guid tipoPatrimonioId);
        public bool StatusPatrimonioExiste(Guid statusPatrimonioId);
        public void Adicionar(Patrimonio patrimonio);
        public void Atualizar(Patrimonio patrimonio);
        public void AtualizarStatus(Patrimonio patrimonio);

    }
}
