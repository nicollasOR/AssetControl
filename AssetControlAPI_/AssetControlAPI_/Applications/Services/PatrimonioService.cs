using AssetControlAPI_.Applications.DTOs.PatrimonioDTO;
using AssetControlAPI_.Applications.Regras;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Exceptions;
using AssetControlAPI_.Interface;

namespace AssetControlAPI_.Applications.Services
{
    public class PatrimonioService
    {

        private readonly IPatrimonioRepository _repository;
        public PatrimonioService(IPatrimonioRepository repository) => _repository = repository;


        public List<ListarPatrimonioDTO> Listar()
        {
            List<Patrimonio> patrimonios = _repository.Listar();

            if (patrimonios == null)
                throw new DomainException("Patrimonios não existem");

            List<ListarPatrimonioDTO> listarDTO = patrimonios.Select(varAux => new ListarPatrimonioDTO
            {
                StatusPatrimonioId = varAux.StatusPatrimonioId,
                TipoPatrimonioId = varAux.TipoPatrimonioId,
                PatrimonioId = varAux.PatrimonioId,
                Valor = varAux.Valor,
                Denominacao = varAux.Denominacao,   
                Imagem = varAux.Imagem, 
                LocalizacaoId = varAux.LocalizacaoId,   
                NumeroSerie = varAux.NumeroSerie
            }
            ).ToList();

            return listarDTO ;
        }

        public ListarPatrimonioDTO BuscarPorId(Guid id)
        {
            Patrimonio? patrimonio = _repository.BuscarPorId(id);

            if (patrimonio == null)
                throw new DomainException("Patrimônio não existe");

            ListarPatrimonioDTO listarDTO = new ListarPatrimonioDTO
            {
                PatrimonioId = patrimonio.PatrimonioId,
                StatusPatrimonioId = patrimonio.StatusPatrimonioId,
                TipoPatrimonioId = patrimonio.TipoPatrimonioId,
                LocalizacaoId = patrimonio.LocalizacaoId,
                Valor = patrimonio.Valor,
                Denominacao = patrimonio.Denominacao,
                Imagem = patrimonio.Imagem,
                NumeroSerie = patrimonio.NumeroSerie
            };

            return listarDTO;
        }


        public ListarPatrimonioDTO BuscarPorNumeroPatrimonio(string numeroPatrimonio, Guid? patrimonioId = null)
        {
            Patrimonio patrimonio = _repository.BuscarPorNumeroPatrimonio(numeroPatrimonio, patrimonioId);
            if (patrimonio == null)
                throw new DomainException("Patrimônio não existe");

            ListarPatrimonioDTO listarDTO = new ListarPatrimonioDTO
            {
                PatrimonioId = patrimonio.PatrimonioId,
                StatusPatrimonioId = patrimonio.StatusPatrimonioId,
                TipoPatrimonioId = patrimonio.TipoPatrimonioId,
                LocalizacaoId = patrimonio.LocalizacaoId,
                Valor = patrimonio.Valor,
                Denominacao = patrimonio.Denominacao,
                Imagem = patrimonio.Imagem,
                NumeroSerie = patrimonio.NumeroSerie
            };

            return listarDTO;

        }

        public void Adicionar(ListarPatrimonioDTO criarDTO)
        {
            ValidarCriacaoDTO.ValidarNome(criarDTO.Denominacao);
            Patrimonio patrimonio = _repository.BuscarPorNumeroPatrimonio(criarDTO.NumeroSerie, criarDTO.PatrimonioId);

            if (patrimonio != null)
                throw new DomainException("Já existe um patrimonio com este nome e numero");

            Patrimonio patrimonioBanco = new Patrimonio
            {
                StatusPatrimonioId = criarDTO.StatusPatrimonioId,
                TipoPatrimonioId = criarDTO.TipoPatrimonioId,
                LocalizacaoId = criarDTO.LocalizacaoId,
                Valor = criarDTO.Valor,
                Denominacao = criarDTO.Denominacao,
                Imagem = criarDTO.Imagem,
                NumeroSerie = criarDTO.NumeroSerie
            };

            _repository.Adicionar(patrimonioBanco);
        }

        public void Atualizar(Guid guid, CriarPatrimonioDTO criarDTO)
        {
            ValidarCriacaoDTO.ValidarNome(criarDTO.Denominacao);
            Patrimonio? patrimonioBanco = _repository.BuscarPorId(guid);
            Patrimonio patrimonio = _repository.BuscarPorNumeroPatrimonio(criarDTO.NumeroSerie, guid);

            patrimonioBanco.StatusPatrimonioId = criarDTO.StatusPatrimonioId;
            patrimonioBanco.TipoPatrimonioId = criarDTO.TipoPatrimonioId;
            patrimonioBanco.LocalizacaoId = criarDTO.LocalizacaoId;
            patrimonioBanco.Valor = criarDTO.Valor;
            patrimonioBanco.Denominacao = criarDTO.Denominacao;
            patrimonioBanco.Imagem = criarDTO.Imagem;
            patrimonioBanco.NumeroSerie = criarDTO.NumeroSerie;

            _repository.Atualizar(patrimonioBanco);
        }

        public void AtualizarStatus(Guid guid, AtualizarStatusPatrimonioDTO criarDTO)
        {
            Patrimonio? patrimonioBanco = _repository.BuscarPorId(guid);
            Patrimonio patrimonio = _repository.BuscarPorNumeroPatrimonio(criarDTO.NumeroSerie, guid);


            patrimonioBanco.StatusPatrimonioId = criarDTO.StatusPatrimonioId;

            _repository.Atualizar(patrimonioBanco);
        }
    }
}
