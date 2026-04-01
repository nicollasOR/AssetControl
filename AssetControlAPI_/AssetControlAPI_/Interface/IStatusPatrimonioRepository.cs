using AssetControlAPI_.Domains;

namespace AssetControlAPI_.Interface
{
    public interface IStatusPatrimonioRepository
    {

        List<StatusPatrimonio> Listar();
        StatusPatrimonio BuscarPorId(Guid statusPatrimonioId);
        StatusPatrimonio BuscarPorNome(string nomeStatus);

        void Adicionar(StatusPatrimonio statusPatrimonio);
        void Atualizar(StatusPatrimonio statusPatrimonio);

    }
}
