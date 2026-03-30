using AssetControlAPI_.Applications.DTOs.LocalizacaoDTO;
using AssetControlAPI_.Applications.Regras;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Exceptions;
using AssetControlAPI_.Interface;

namespace AssetControlAPI_.Applications.Services
{
    public class LocalizacaoService
    {

        private readonly ILocalizacaoRepository _repository;
        public LocalizacaoService(ILocalizacaoRepository repository) => _repository = repository;



        public List<ListarLocalizacaoDTO> Listar()
        {
            List<Localizacao> localizacoes = _repository.Listar();

            List<ListarLocalizacaoDTO> localizacoesDTOs = localizacoes.Select
                (localizacaoConstrutor => new ListarLocalizacaoDTO
                {
                    LocalizacaoId = localizacaoConstrutor.LocalizacaoId,
                    nomeLocal = localizacaoConstrutor.NomeLocalizacao,
                    localSAP = localizacaoConstrutor.LocalizacaoSAP,
                    DescricaoSAP = localizacaoConstrutor.DescricaoSAP,
                    AreaId  = localizacaoConstrutor.AreaId

                }).ToList();

            return localizacoesDTOs;    
        }

        public ListarLocalizacaoDTO ObterPorGuid(Guid guid)
        {
            Localizacao local = _repository.ObterPorGuid(guid);
            if (local == null)
                throw new DomainException("Localização não existe");

            ListarLocalizacaoDTO localDTO = new ListarLocalizacaoDTO
            {
                LocalizacaoId = local.LocalizacaoId,
                AreaId = local.AreaId,
                nomeLocal = local.NomeLocalizacao,
                DescricaoSAP = local.DescricaoSAP,
                localSAP = local.LocalizacaoSAP
            };

            return localDTO;
        }


        public void  Adicionar(CriarLocalizacaoDTO criarDTO)
        {
            ValidarCriacaoDTO.ValidarNome(criarDTO.nomeLocal);
            Localizacao localExiste = _repository.ObterPorNome(criarDTO.nomeLocal, criarDTO.AreaId);
            if (localExiste != null)
                throw new DomainException("Já existe um local cadastrado com esse nome área");

            if (_repository.AreaExiste(criarDTO.AreaId) == false)
                throw new DomainException("A área informada não existe");
            Localizacao localizacao = new Localizacao
            {
                NomeLocalizacao = criarDTO.nomeLocal,
                LocalizacaoSAP = criarDTO.localSAP,
                DescricaoSAP = criarDTO.DescricaoSAP,
                AreaId = criarDTO.AreaId
            };

            _repository.Adicionar
                (localizacao);
        }


        public void Atualizar(Guid guid, CriarLocalizacaoDTO listarDTO)
        {
            ValidarCriacaoDTO.ValidarNome(listarDTO.nomeLocal);
            Localizacao localizacaoBanco = _repository.ObterPorGuid(guid);

            Localizacao localExiste = _repository.ObterPorNome(listarDTO.nomeLocal, listarDTO.AreaId);
            if (localExiste != null)
                throw new DomainException("Já existe um local cadastrado com o nome dessa área");


            if (localizacaoBanco == null)
                throw new DomainException("Localização não encontrada");

            if (!_repository.AreaExiste(listarDTO.AreaId))
                throw new DomainException("Área não existe");

            localizacaoBanco.AreaId = listarDTO.AreaId;
            localizacaoBanco.LocalizacaoSAP = listarDTO.localSAP;
            localizacaoBanco.NomeLocalizacao = listarDTO.nomeLocal;
            localizacaoBanco.DescricaoSAP = listarDTO.DescricaoSAP;

            _repository.Atualizar(localizacaoBanco);

        }

            }

}
