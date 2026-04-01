using AssetControlAPI_.Domains;

namespace AssetControlAPI_.Interface
{
    public interface IStatusTransferenciaRepository
    {

        List<StatusTransferencia> Listar();
        StatusTransferencia BuscarPorId(Guid statusTransferenciaId);
        StatusTransferencia BuscarPorNome(string nomeStatus);

        void Adicionar(StatusTransferencia statusTransferencia);
        void Atualizar(StatusTransferencia statusTransferencia);

    }
}
