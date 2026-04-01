using AssetControlAPI_.Domains;

namespace AssetControlAPI_.Interface
{
    public interface ICargoRepository
    {

        public List<Cargo> Listar();
        public Cargo BuscarPorId(Guid cargoId);
        public Cargo BuscarPorNome(string nomeCargo);
        public void Adicionar(Cargo cargo);
        public void Atualizar(Cargo cargo);

    }
}
