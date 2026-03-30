using AssetControlAPI_.Contexts;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Interface;
using System.Diagnostics.Eventing.Reader;

namespace AssetControlAPI_.Repository
{
    public class BairroRepository : IBairroRepository
    {

        private readonly AssetDBContext _context;
        public BairroRepository (AssetDBContext context) => _context = context;


        public List<Bairro> Listar()
        {
            return _context.Bairro.OrderBy(bairroAux => bairroAux.NomeBairro).ToList();
        }

        public Bairro ObterPorGuid(Guid bairroId)
        {
            var busca =  _context.Bairro.Find(bairroId);
            if (busca == null)
                    Console.WriteLine("faz o L");
            return busca;
        }

        public Bairro ObterPorNomeEstado(string nomeBairro, Guid cidadeId)
        {
            return _context.Bairro.FirstOrDefault(varAux => varAux.NomeBairro == nomeBairro && varAux.CidadeId == cidadeId);
            
        }

       public bool CidadeExiste(Guid bairroId)
        {
            return _context.Bairro.Any(aux => aux.BairroId == bairroId);
        }

        public void Adicionar(Bairro bairro)
        {
            _context.Bairro.Add(bairro);
            _context.SaveChanges();
        }
        public void Atualizar(Bairro bairro)
        {
            Bairro bairroAux = _context.Bairro.Find(bairro.BairroId);
            if (bairroAux == null)
                return;

            bairroAux.NomeBairro = bairro.NomeBairro;
            bairroAux.CidadeId = bairro.CidadeId;

            _context.SaveChanges();
        }
    }
}
