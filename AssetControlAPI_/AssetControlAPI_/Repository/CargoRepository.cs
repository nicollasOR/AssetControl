using AssetControlAPI_.Contexts;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Interface;

namespace AssetControlAPI_.Repository
{
    public class CargoRepository : ICargoRepository
    {

        private readonly AssetDBContext _context;
        public CargoRepository(AssetDBContext context) => _context = context;

        public List<Cargo> Listar()
        {
            return _context.Cargo.OrderBy(varAux => varAux.Nome).ToList();
        }

        public Cargo BuscarPorId(Guid guid)
        {
            return _context.Cargo.Find(guid);
        }

        public Cargo BuscarPorNome(string nomeCargo)
        {
            return _context.Cargo.FirstOrDefault(varAux => varAux.Nome == nomeCargo);
        }

        public void Adicionar(Cargo cargo)
        {
            _context.Cargo.Add(cargo);
            _context.SaveChanges(); 
        }

        public void Atualizar(Cargo cargo)
        {
            if (cargo == null)
                return;

           
            Cargo? cargoBanco = _context.Cargo.Find(cargo.CargoId);

            cargoBanco.Usuario = cargo.Usuario;
            cargoBanco.Nome = cargo.Nome;

            _context.SaveChanges(); 

        }

    }
}
