using AssetControlAPI_.Contexts;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Interface;
using Microsoft.EntityFrameworkCore;

namespace AssetControlAPI_.Repository
{
    public interface IEnderecoRepository
    {
       public List<Endereco> Listar();
       public Endereco BuscarPorId(Guid enderecoId);
       public void Adicionar(Endereco endereco);
       public void Atualizar(Endereco endereco);
       public bool BairroExiste(Guid bairroId);
        public Endereco BuscarPorLogradouro(string logradoura, int? numero, Guid bairroId);
       public Endereco BuscarPorLogradouroENumero(string logradouro, int? numero, Guid bairroId, Guid? enderecoId = null);

    }
}
