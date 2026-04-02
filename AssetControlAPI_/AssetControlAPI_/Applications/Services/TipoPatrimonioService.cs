using AssetControlAPI_.Applications.DTOs.EndereçoDTO;
using AssetControlAPI_.Applications.DTOs.TipoPatrimonioDTO;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Interface;
using AssetControlAPI_.Exceptions;
using AssetControlAPI_.Applications.Regras;

namespace AssetControlAPI_.Applications.Services
{
    public class TipoPatrimonioService
    {

        private readonly ITipoPatrimonioRepository _repository;
        public TipoPatrimonioService(ITipoPatrimonioRepository repository) => _repository = repository;

        private static ListarTipoPatrimonioDTO lerDTO(TipoPatrimonio tipoPatrimonio)
        {
            return new ListarTipoPatrimonioDTO
            {
                tipoPatrimonioID = tipoPatrimonio.TipoPatrimonioId,
                nomeTipoPatrimonio = tipoPatrimonio.NomeTipo
            };
        }
        public List<ListarTipoPatrimonioDTO> Listar()
        {
            List<TipoPatrimonio> tipoPatrimonio = _repository.Listar();
            if (tipoPatrimonio == null)
                throw new DomainException("não existe este tipo");

            List<ListarTipoPatrimonioDTO> tipoDTO = tipoPatrimonio.Select(tipoDTOs => lerDTO(tipoDTOs)).ToList();

            return tipoDTO;
        }

        public ListarTipoPatrimonioDTO ObterPorId(Guid id)
        {
            TipoPatrimonio tipoPatrimonio = _repository.ObterPorId(id);
            if (tipoPatrimonio == null)
                throw new DomainException("TipoPatrimônio não existente");

            ListarTipoPatrimonioDTO tipoPatrimonioDTO = lerDTO(tipoPatrimonio);
            return tipoPatrimonioDTO;
        }

        public ListarTipoPatrimonioDTO ObterPorNome(string nome)
        {
            TipoPatrimonio tipoPatrimonio = _repository.ObterPorNome(nome);
            if(tipoPatrimonio == null)
                throw new DomainException("TipoPatrimônio com nome errado não");

            ListarTipoPatrimonioDTO tipoPatrimonioDTO = lerDTO(tipoPatrimonio);
            return tipoPatrimonioDTO;
        }

        public void Adicionar(CriarTipoPatrimonioDTO criarDTO)
        {
            //if(_repository.ObterPorNome(criarDTO.nomeTipoPatrimonio) == null)
            ValidarCriacaoDTO.ValidarNome(criarDTO.nomeTipoPatrimonio);
            TipoPatrimonio tipoPatrimonio = _repository.ObterPorNome(criarDTO.nomeTipoPatrimonio);
            if (tipoPatrimonio != null)
                throw new DomainException("Já existe um tipoPatrimonio com este nome");

            TipoPatrimonio criarDTOs = new TipoPatrimonio
            {
                NomeTipo = criarDTO.nomeTipoPatrimonio
            };

            _repository.Adicionar(criarDTOs);
        }

        public void Atualizar(Guid id, CriarTipoPatrimonioDTO atualizarDTO)
        {
            ValidarCriacaoDTO.ValidarNome(atualizarDTO.nomeTipoPatrimonio);
            TipoPatrimonio tipoPatrimonioBanco = _repository.ObterPorNome(atualizarDTO.nomeTipoPatrimonio);
            TipoPatrimonio tipoPatrimonioExiste = _repository.ObterPorId(id);

            if (tipoPatrimonioExiste == null)
                throw new DomainException("TipoPatrimônio não existe");
            if (tipoPatrimonioBanco != null)
                throw new DomainException("Já existe um TipoPatrimônio com esse nome");

            tipoPatrimonioExiste.NomeTipo = atualizarDTO.nomeTipoPatrimonio;

            _repository.Atualizar(tipoPatrimonioExiste);
        }

    }
}
