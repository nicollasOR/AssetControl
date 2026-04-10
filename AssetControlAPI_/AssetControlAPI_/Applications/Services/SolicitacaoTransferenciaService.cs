using AssetControlAPI_.Applications.DTOs.SolicitacaoTransferenciaDTO;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Exceptions;
using AssetControlAPI_.Interface;
using AssetControlAPI_.Repository;
using System.ComponentModel.DataAnnotations;
using AssetControlAPI_.Applications.Regras;


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
                TransferenciaId = sTransferencia.SolicitacaoTransId,
                DataCriacaoSolicitante = sTransferencia.DataSolicitacao,
                DataResposta = sTransferencia.DataResposta,
                Justificativa = sTransferencia.Justificativa,
                localizacaoId = sTransferencia.LocalizacaoId,
                patrimonioId = sTransferencia.PatrimonioId,
                StatusTransferenciaId = sTransferencia.StatusTransferenciaId,
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
            //                      ?
            SolicitacaoTransferencia domainsSolicitacao = _repository.BuscarPorId(id);
            if (domainsSolicitacao == null)
                throw new DomainException("Solicitação Transferência não encontrada");

            ListarSolicitacaoTransferenciaDTO listarDTO = lerDTO(domainsSolicitacao);

            return listarDTO;
        }



        public void Adicionar(Guid usuarioId, CriarSolicitacaoTransferenciaDTO dto)
        {
            ValidarCriacaoDTO.ValidarJustificativa(dto.Justificativa);

            Usuario usuario = _repositoryUsuario.BuscarPorId(usuarioId);

            if (usuario == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            Patrimonio patrimonio = _repository.BuscarPatrimonioPorId(dto.patrimonioId);

            if (patrimonio == null)
            {
                throw new DomainException("Patrimônio não encontrado.");
            }

            if (!_repository.LocalizacaoExiste(dto.localizacaoId))
            {
                throw new DomainException("Localização não encontrada.");
            }

            if (patrimonio.LocalizacaoId == dto.localizacaoId)
            {
                throw new DomainException("O patrimônio já está nessa localização.");
            }

            if (_repository.ExisteSolicitacaoPendente(dto.patrimonioId))
            {
                throw new DomainException("Já existe uma solicitação pendente para esse patrimônio");
            }

            if (usuario.TipoUsuario.Nome == "Responsável")
            {
                bool usuarioResponsavel = _repository.UsuarioResponsavelDaLocalizacao(usuarioId, patrimonio.LocalizacaoId);

                if (!usuarioResponsavel) // Caso retorne "false".
                {
                    throw new DomainException("O responsável só pode solicitar transferência de patrimônio do ambiente ao qual está vinculado.");
                }
            }

            StatusTransferencia statusPendente = _repository.BuscarStatusTransferenciaPorNome("Pendente de aprovação.");

            if (statusPendente == null)
            {
                throw new DomainException("Status de transferência pendente não encontrado.");
            }

            SolicitacaoTransferencia solicitacao = new SolicitacaoTransferencia
            {
                DataSolicitacao = DateTime.Now,
                Justificativa = dto.Justificativa,
                StatusTransferenciaId = statusPendente.StatusTransferenciaId,
                UsuarioSolicitandoId = usuarioId,
                UsuarioAprovacaoId = null,
                PatrimonioId = dto.patrimonioId,
                LocalizacaoId = dto.localizacaoId,
            };

            _repository.Adicionar(solicitacao);
        }

        public void Responder(Guid transferenciaId, Guid usuarioId, ResponderSolicitacaoTransferenciaDTO dto)
        {
            Usuario usuario = _repositoryUsuario.BuscarPorId(usuarioId);


            if (usuario == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            SolicitacaoTransferencia solicitacao = _repository.BuscarPorId(transferenciaId);

            if (solicitacao == null)
            {
                throw new DomainException("Solicitação de transferência não encontrada.");
            }

            Patrimonio patrimonio = _repository.BuscarPatrimonioPorId(solicitacao.PatrimonioId);

            if (patrimonio == null)
            {
                throw new DomainException("Patrimônio não encontrado.");
            }

            StatusTransferencia statusPendente = _repository.BuscarStatusTransferenciaPorNome("Pendente de aprovação.");

            if (statusPendente == null)
            {
                throw new DomainException("Status pendente não encontrada.");
            }

            if (solicitacao.StatusTransferenciaId != statusPendente.StatusTransferenciaId)
            {
                throw new DomainException("Essa solicitação já foi respondida.");
            }

            if (usuario.TipoUsuario.Nome == "Responsável")
            {
                bool usuarioResponsavel = _repository.UsuarioResponsavelDaLocalizacao(usuarioId, patrimonio.LocalizacaoId);

                if (!usuarioResponsavel)
                {
                    throw new DomainException("Somente o responsável do ambiente de origem pode aprovar ou rejeitar essa solicitação.");
                }

                StatusTransferencia statusResposta;

                if (dto.Aprovado)
                {
                    statusResposta = _repository.BuscarStatusTransferenciaPorNome("Aprovado");
                }
                else
                {
                    statusResposta = _repository.BuscarStatusTransferenciaPorNome("Recusado");
                }

                if (statusResposta == null)
                {
                    throw new DomainException("Status de resposta da transferência não encontrado.");
                }

                solicitacao.StatusTransferenciaId = statusResposta.StatusTransferenciaId;
                solicitacao.UsuarioAprovacaoId = usuarioId;
                solicitacao.DataResposta = DateTime.Now;

                _repository.Atualizar(solicitacao);
            }

            if (dto.Aprovado)
            {
                StatusPatrimonio statusTransferido = _repository.BuscarStatusPatrimonioPorNome("Transferido");

                if (statusTransferido == null)
                {
                    throw new DomainException("Status de patrimônio 'Transferido' não encontrado.");
                }

                TipoAlteracao tipoAlteracao = _repository.BuscarTipoAlteracaoPorNome("Transferência");

                if (tipoAlteracao == null)
                {
                    throw new DomainException("Tipo alteração 'Transferência' não encontrado.");
                }

                patrimonio.LocalizacaoId = solicitacao.LocalizacaoId;
                patrimonio.StatusPatrimonioId = statusTransferido.StatusPatrimonioId;

                _repository.AtualizarPatrimonio(patrimonio);

                LogPatrimonio log = new LogPatrimonio
                {
                    DataTransferencia = DateTime.Now,
                    TipoAlteracaoId = tipoAlteracao.TipoAlteracaoId,
                    StatusPatrimonioId = statusTransferido.StatusPatrimonioId,
                    PatrimonioId = patrimonio.PatrimonioId,
                    UsuarioId = usuarioId,
                    LocalizacaoId = patrimonio.LocalizacaoId,
                };

                _repository.AdicionarLog(log);
            }
        }
    }
}
