using AssetControlAPI_.Domains;

namespace AssetControlAPI_.Interface
{
    public interface IStatusTransferenciaRepository
    {

        public List<StatusTransferencia> Listar();
        public StatusTransferencia BuscarPorId(Guid statusTransferenciaId);
        public StatusTransferencia BuscarPorNome(string nomeStatus);
        public void Adicionar(StatusTransferencia statusTransferencia);
        public void Atualizar(StatusTransferencia statusTransferencia);

    }
}
