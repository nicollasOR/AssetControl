using AssetControlAPI_.Domains;

namespace AssetControlAPI_.Interface
{
    public interface IStatusPatrimonioRepository
    {

       public List<StatusPatrimonio> Listar();
       public StatusPatrimonio BuscarPorId(Guid statusPatrimonioId);
       public StatusPatrimonio BuscarPorNome(string nomeStatus);
       public void Adicionar(StatusPatrimonio statusPatrimonio);
       public void Atualizar(StatusPatrimonio statusPatrimonio);

    }
}
