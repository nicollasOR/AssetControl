using AssetControlAPI_.Applications.DTOs.EndereçoDTO;
using AssetControlAPI_.Repository;
using AssetControlAPI_.Domains;

namespace AssetControlAPI_.Applications.Services
{
    public class EnderecoService
    {
        private readonly IEnderecoRepository _repository;
        public EnderecoService(IEnderecoRepository repository) => _repository = repository;

        public List<LerEnderecoDTO> Listar()
        {
            List<Endereco> Enderecos = _repository.Listar();

            List<LerEnderecoDTO> listarDTO = Enderecos.Select(varAux => new LerEnderecoDTO
            { 
              Logradoura = varAux.Logradoura,
              bairroId = varAux.BairroId,
              CEP = varAux.CEP,
              Complemento = varAux.Complemento,
              enderecoId = varAux.EnderecoId
            }).ToList();

            return listarDTO;
        }

        public LerEnderecoDTO BuscarPorId(Guid enderecoId)
        {

        }

    }
}
