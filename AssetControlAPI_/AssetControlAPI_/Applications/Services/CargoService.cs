using AssetControlAPI_.Applications.DTOs.CargoDTO;
using AssetControlAPI_.Applications.Regras;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Exceptions;
using AssetControlAPI_.Interface;

namespace AssetControlAPI_.Applications.Services
{
    public class CargoService
    {
        private readonly ICargoRepository _repository;
        public CargoService(ICargoRepository repository) => _repository = repository;

        public List<ListarCargoDTO> Listar()
        {

            List<Cargo> cargoDomain = _repository.Listar();
            if (cargoDomain == null)
                throw new DomainException("Cargo não encontrado");

            List<ListarCargoDTO> listarDTO = cargoDomain.Select(x => new ListarCargoDTO 
            {
            CargoId = x.CargoId, 
            CargoNome = x.Nome 
            }).ToList();

            return listarDTO;
        }

        public ListarCargoDTO BuscarPorId (Guid id)
        {
            Cargo? cargo = _repository.BuscarPorId(id);
            if (cargo == null)
                throw new DomainException("Cargo não encontrado");

            ListarCargoDTO listar = new ListarCargoDTO
            {
                CargoId = cargo.CargoId,
                CargoNome = cargo.Nome
            };

            return listar;

        }

        public ListarCargoDTO BuscarPorNome(string nomeCargo)
        {
            Cargo? cargo = _repository.BuscarPorNome(nomeCargo);

            if (cargo == null)
                throw new DomainException("Cargo não encontrado");

            ListarCargoDTO listarDTO = new ListarCargoDTO
            {
                CargoId = cargo.CargoId,
                CargoNome = cargo.Nome
            };

            return listarDTO;
        }


        public void Adicionar(CriarCargoDTO criarCargo)
        {
            ValidarCriacaoDTO.ValidarNome(criarCargo.Cargo);
            Cargo? cargoBanco = _repository.BuscarPorNome(criarCargo.Cargo);

            if (cargoBanco == null)
                throw new DomainException("cargo não existe");

            Cargo cargoDTO = new Cargo
            {
                Nome = criarCargo.Cargo
            };

            _repository.Adicionar(cargoDTO);
        }

        public void Atualizar(Guid id, CriarCargoDTO criarDTO)
        {
            ValidarCriacaoDTO.ValidarNome(criarDTO.Cargo);
            Cargo? cargo = _repository.BuscarPorNome(criarDTO.Cargo);

            if (cargo == null)
                throw new DomainException("Este cargo não existe");

            cargo.Nome = criarDTO.Cargo;

            _repository.Atualizar(cargo);

        }
    }
}
