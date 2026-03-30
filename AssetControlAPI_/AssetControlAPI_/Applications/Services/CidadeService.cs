using AssetControlAPI_.Domains;
using AssetControlAPI_.Interface;
using AssetControlAPI_.Applications.DTOs.CidadeDTO;
using AssetControlAPI_.Exceptions;
using AssetControlAPI_.Applications.Regras;

namespace AssetControlAPI_.Applications.Services
{
    public class CidadeService
    {

        private readonly ICidadeRepository _repository;
        public CidadeService(ICidadeRepository repository) => _repository = repository;


        public List<ListarCidadeDTO> Listar()
        {

            List<Cidade> cidades = _repository.Listar();

            List<ListarCidadeDTO> cidadesDto = cidades.Select(cidade => new ListarCidadeDTO
            {
                cidadeId = cidade.CidadeId,
                cidadeNome = cidade.NomeCidade,
                estadoNome = cidade.Estado
            }).ToList();

            return cidadesDto;
        }
        
        public ListarCidadeDTO BuscarPorId(Guid  cidadeId)
        {
            Cidade? cidade = _repository.ObterPorId(cidadeId);

            if (cidade == null)
                throw new DomainException("Área não existe");

            ListarCidadeDTO listarDto = new ListarCidadeDTO
            {
                cidadeId = cidade.CidadeId,
                cidadeNome = cidade.NomeCidade,
                estadoNome = cidade.Estado
            };

            return listarDto;
        }

        public void Adicionar(CriarCidadeDTO criarDTO)
        {
            ValidarCriacaoDTO.ValidarNome(criarDTO.NomeCidade);
            ValidarCriacaoDTO.ValidarEstado(criarDTO.nomeEstado);

            Cidade? cidadeBanco = _repository.ObterPorNomeEstado(criarDTO.NomeCidade, criarDTO.nomeEstado);
            if (cidadeBanco != null)
                throw new DomainException("Já existe esta cidade");

            Cidade cidade = new Cidade
            {
                NomeCidade = criarDTO.NomeCidade,
                Estado = criarDTO.nomeEstado
            };

            _repository.Adicionar(cidade);
        }

        public void Atualizar( Guid cidadeId,CriarCidadeDTO listarDto)
        {
            ValidarCriacaoDTO.ValidarNome(listarDto.NomeCidade);
            ValidarCriacaoDTO.ValidarEstado(listarDto.nomeEstado);

            Cidade? cidadeBanco = _repository.ObterPorNomeEstado(listarDto.NomeCidade, listarDto.nomeEstado);

            if (cidadeBanco == null)
                throw new DomainException("Cidade não existe");

            if (cidadeBanco != null && cidadeBanco.CidadeId != cidadeId)
            
                throw new DomainException("Já existe uma cidade cadastrada com esse nome nesse estado.");

            cidadeBanco.NomeCidade = listarDto.NomeCidade;
            cidadeBanco.Estado = listarDto.nomeEstado;

            _repository.Atualizar(cidadeBanco);
        }

    }
}
