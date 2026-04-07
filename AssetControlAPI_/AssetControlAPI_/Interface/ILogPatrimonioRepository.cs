using AssetControlAPI_.Domains;

namespace AssetControlAPI_.Interface
{
    public interface ILogPatrimonioRepository
    {

        public List<LogPatrimonio> Listar();
        public List<LogPatrimonio> BuscarPorPatrimonio(Guid patrimonioId);

    }
}
