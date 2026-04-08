using AssetControlAPI_.Applications.DTOs.SolicitacaoTransferenciaDTO;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Exceptions;
using AssetControlAPI_.Interface;

namespace AssetControlAPI_.Applications.Services
{
    public class SolicitacaoTransferenciaService
    {
        private readonly ISolicitacaoTransferenciaRepository _repository;
        private readonly IUsuarioRepository _repositoryUsuario;
        public SolicitacaoTransferenciaService(ISolicitacaoTransferenciaRepository repository, IUsuarioRepository repositoryUsuario)
        {
            _repository = repository;
            _repositoryUsuario = repositoryUsuario;
        }

        private static ListarSolicitacaoTransferenciaDTO lerDTO(SolicitacaoTransferencia sTransferencia)
        {
            return new ListarSolicitacaoTransferenciaDTO
            {
                SolicitacaoTransferenciaId = sTransferencia.SolicitacaoTransId,
                DataCriacaoSolicitante = sTransferencia.DataSolicitacao,
                DataResposta = sTransferencia.DataResposta,
                Justificativa = sTransferencia.Justificativa,
                localizacaoId = sTransferencia.LocalizacaoId,
                patrimonioId = sTransferencia.PatrimonioId,
                //Transferenciaid = sTransferencia.
                UsuarioIdAprovacao = sTransferencia.UsuarioAprovacaoId,
                UsuarioIdSolicitacao = sTransferencia.UsuarioSolicitandoId
            };
        }

        public List<ListarSolicitacaoTransferenciaDTO> Listar()
        {
            List<SolicitacaoTransferencia> domainList = _repository.Listar();

            List<ListarSolicitacaoTransferenciaDTO> solicitacoesDTO = domainList.Select(varAux => lerDTO(varAux)).ToList();
            return solicitacoesDTO;
        }

        public ListarSolicitacaoTransferenciaDTO BuscarPorId(Guid id)
        {
            SolicitacaoTransferencia? domainsSolicitacao = _repository.BuscarPorId(id);
            if (domainsSolicitacao == null)
                throw new DomainException("Solicitação Transferência não encontrada");

            ListarSolicitacaoTransferenciaDTO listarDTO = lerDTO(domainsSolicitacao);

            return listarDTO;
        }


    }
}
