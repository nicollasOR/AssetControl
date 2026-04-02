using AssetControlAPI_.Contexts;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Interface;
using Microsoft.EntityFrameworkCore;

namespace AssetControlAPI_.Repository
{
    public class EnderecoRepository : IEnderecoRepository
    {

        private readonly AssetDBContext _context;
        public EnderecoRepository(AssetDBContext context) => _context = context;

        public List<Endereco> Listar()
        {
            return _context.Endereco.OrderBy(aux => aux.Logradoura).ToList();
        }

        public Endereco BuscarPorId(Guid enderecoId)
        {
            return _context.Endereco.Find(enderecoId);
        }

        public bool BairroExiste(Guid bairroId)
        {
            return _context.Endereco.Any(aux => aux.BairroId == bairroId);
        }
        public Endereco BuscarPorLogradouro(string logradoura, int? numero, Guid bairroId)
        {
            return _context.Endereco.FirstOrDefault(varAux => varAux.Logradoura.ToLower() == logradoura.ToLower() && varAux.Numero == numero && varAux.BairroId == bairroId);
        }


        public Endereco BuscarPorLogradouroENumero(string logradouro, int? numero, Guid bairroId, Guid? enderecoId = null)
        {
            var consulta = _context.Endereco.AsQueryable();
            if (enderecoId.HasValue)
                consulta = consulta.Where(endereco => endereco.EnderecoId != enderecoId.Value);

            return consulta.FirstOrDefault(
                endereco =>
                endereco.Logradoura.ToLower() == logradouro.ToLower() &&
                endereco.Numero == numero &&
                endereco.BairroId == bairroId
                );

        }

        public void Adicionar(Endereco endereco)
        {
            _context.Endereco.Add(endereco);
            _context.SaveChanges(); 
        }
        
        public void Atualizar(Endereco endereco)
        {
            if (endereco == null)
                return;

            Endereco? enderecoBanco = _context.Endereco.Find(endereco.EnderecoId);
            if (enderecoBanco == null)
                return;

            enderecoBanco.Logradoura = endereco.Logradoura;
            enderecoBanco.Numero = endereco.Numero;
            enderecoBanco.CEP = endereco.CEP;
            enderecoBanco.BairroId = endereco.BairroId;
            enderecoBanco.Complemento = endereco.Complemento;

            _context.SaveChanges();
        }

    }
}
