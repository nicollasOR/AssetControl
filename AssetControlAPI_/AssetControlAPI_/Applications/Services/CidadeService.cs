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

        private static ListarCidadeDTO listarDTO(Cidade cidade)
        {
            return new ListarCidadeDTO
            {
                cidadeId = cidade.CidadeId,
                cidadeNome = cidade.NomeCidade,
            };
        }

        public List<ListarCidadeDTO> Listar()
        {

            List<Cidade> cidades = _repository.Listar();

            List<ListarCidadeDTO> cidadesDto = cidades.Select(cidade => new ListarCidadeDTO
            {
                cidadeId = cidade.CidadeId,
                cidadeNome = cidade.NomeCidade,
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
            };

            return listarDto;
        }

        public void Adicionar(CriarCidadeDTO criarDTO)
        {
            ValidarCriacaoDTO.ValidarNome(criarDTO.NomeCidade);

            Cidade? cidadeBanco = _repository.ObterPorNome(criarDTO.NomeCidade);
            if (cidadeBanco != null)
                throw new DomainException("Já existe esta cidade");

            Cidade cidade = new Cidade
            {
                NomeCidade = criarDTO.NomeCidade
            };

            _repository.Adicionar(cidade);
        }

        public void Atualizar( Guid cidadeId,CriarCidadeDTO listarDto)
        {
            ValidarCriacaoDTO.ValidarNome(listarDto.NomeCidade);

            Cidade? cidadeBanco = _repository.ObterPorNome(listarDto.NomeCidade);

            if (cidadeBanco == null)
                throw new DomainException("Cidade não existe");

            if (cidadeBanco != null && cidadeBanco.CidadeId != cidadeId)
            
                throw new DomainException("Já existe uma cidade cadastrada com esse nome nesse estado.");

            cidadeBanco.NomeCidade = listarDto.NomeCidade;

            _repository.Atualizar(cidadeBanco);
        }

    }
}
