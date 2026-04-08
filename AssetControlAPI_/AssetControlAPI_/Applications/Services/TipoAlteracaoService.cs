using AssetControlAPI_.Applications.DTOs.TipoAlteracaoDTO;
using AssetControlAPI_.Applications.Regras;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Exceptions;
using AssetControlAPI_.Interface;

namespace AssetControlAPI_.Applications.Services
{
    public class TipoAlteracaoService
    {
        private readonly ITipoAlteracaoRepository _repository;
        public TipoAlteracaoService(ITipoAlteracaoRepository repository) => _repository = repository;

        private static ListarTipoAlteracaoDTO lerDTO(TipoAlteracao tipoAlteracao)
        {
            return new ListarTipoAlteracaoDTO
            {
                TipoAlteracaoId = tipoAlteracao.TipoAlteracaoId,
                nome = tipoAlteracao.NomeAlteracao
            };
        }
        public List<ListarTipoAlteracaoDTO> Listar()
        {
            List<TipoAlteracao> tipoAlteracao = _repository.Listar();

            List<ListarTipoAlteracaoDTO> listarDTO = tipoAlteracao.Select(varAux => lerDTO(varAux)).ToList();
            return listarDTO;
        }

        public ListarTipoAlteracaoDTO BuscarPorId(Guid id)
        {
            TipoAlteracao? tipoAlteracao = _repository.BuscarPorId(id);

            if (tipoAlteracao == null)
                throw new DomainException("Tipo Alteração não encontrada");

            ListarTipoAlteracaoDTO listarDTO = lerDTO(tipoAlteracao);
            return listarDTO;
        }

        public ListarTipoAlteracaoDTO BuscarPorNome(string nome)
        {
            TipoAlteracao? tipoAlteracao = _repository.BuscarPorNome(nome);
            if (tipoAlteracao == null)
                throw new DomainException("Tipo Alteração não encontrada");

            ListarTipoAlteracaoDTO listarDTO = lerDTO(tipoAlteracao);
            return listarDTO;
        }

        public void Adicionar(CriarTipoAlteracaoDTO criarDTO)
        {
            ValidarCriacaoDTO.ValidarNome(criarDTO.nome);

            TipoAlteracao? tipoAlteracao = _repository.BuscarPorNome(criarDTO.nome);
            if (tipoAlteracao != null)
                throw new DomainException("Tipo Alteração já existe");

            TipoAlteracao tipoAlteracaoDTO = new TipoAlteracao
            {
                NomeAlteracao = criarDTO.nome
            };

            _repository.Adicionar(tipoAlteracaoDTO);

        }


        public void Atualizar(Guid id, CriarTipoAlteracaoDTO criarDTO)
        {
            ValidarCriacaoDTO.ValidarNome(criarDTO.nome);

            TipoAlteracao tipoAlteracao = _repository.BuscarPorNome(criarDTO.nome);
            TipoAlteracao tipoAlteracaoBanco = _repository.BuscarPorId(id);
            if (tipoAlteracao == null && tipoAlteracaoBanco == null)
                throw new DomainException("Tipo Alteração não encontrada");

            tipoAlteracaoBanco.NomeAlteracao = criarDTO.nome;
            _repository.Atualizar(tipoAlteracao);
        }
    }
}
