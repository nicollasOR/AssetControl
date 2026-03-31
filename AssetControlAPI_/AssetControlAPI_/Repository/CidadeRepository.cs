using AssetControlAPI_.Contexts;
using AssetControlAPI_.Domains;

namespace AssetControlAPI_.Interface
{
    public class CidadeRepository : ICidadeRepository
    {
        private readonly AssetDBContext _context;
        public CidadeRepository(AssetDBContext context) => _context = context;

        public List<Cidade> Listar()
        {
            return _context.Cidade.OrderBy(varAux => varAux.NomeCidade).ToList();
        }

        public Cidade ObterPorId(Guid cidadeId)
        {
            
            var busca = _context.Cidade.Find(cidadeId);
            if (busca == null)
                Console.WriteLine("CidadeId é nulo");

            return busca;
        }

        public Cidade ObterPorNomeEstado(string nomeCidade, string nomeEstado)
        {
            var busca = _context.Cidade.FirstOrDefault(varAux => varAux.NomeCidade == nomeCidade && varAux.Estado == nomeEstado);
            return busca;
        }

        public void Adicionar(Cidade cidade)
        {
            _context.Cidade.Add(cidade);
            _context.SaveChanges();
        }

        public void Atualizar(Cidade cidade)
        {
            if (cidade == null)
                return;
            Cidade cidadeBanco = _context.Cidade.Find(cidade.CidadeId);

            cidadeBanco.NomeCidade = cidade.NomeCidade;
            cidadeBanco.Estado = cidade.Estado;


            _context.SaveChanges();

            
        }




    }
}
