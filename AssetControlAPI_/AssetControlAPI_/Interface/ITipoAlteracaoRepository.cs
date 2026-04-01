using AssetControlAPI_.Domains;

namespace AssetControlAPI_.Interface
{
    public interface ITipoAlteracaoRepository
    {

        List<TipoAlteracao> Listar();
        TipoAlteracao BuscarPorId(Guid tipoAlteracaoId);
        TipoAlteracao BuscarPorNome(string nomeTipo);

        void Adicionar(TipoAlteracao tipoAlteracao);
        void Atualizar(TipoAlteracao tipoAlteracao);

    }
}
