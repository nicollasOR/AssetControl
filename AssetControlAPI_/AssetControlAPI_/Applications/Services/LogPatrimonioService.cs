using AssetControlAPI_.Applications.DTOs.LogPatrimonioDTO;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Exceptions;
using AssetControlAPI_.Interface;

namespace AssetControlAPI_.Applications.Services
{
    public class LogPatrimonioService
    {

        private readonly ILogPatrimonioRepository _repository;
        public LogPatrimonioService(ILogPatrimonioRepository repository) => _repository = repository;   

        private static ListarLogPatrimonioDTO lerDTO(LogPatrimonio logPatrimonio)
        {
            return new ListarLogPatrimonioDTO
            {
                Usuario = logPatrimonio.Usuario.Nome,
                Localizacao = logPatrimonio.Localizacao.NomeLocalizacao,
                DataTransferencia = logPatrimonio.DataTransferencia,
                LogPatrimonioId = logPatrimonio.LogPatrimonioId,
                PatrimonioId = logPatrimonio.PatrimonioId,
                StatusPatrimonio = logPatrimonio.StatusPatrimonio.NomeStatusPatrimonio,
                TipoAlteracao = logPatrimonio.TipoAlteracao.NomeAlteracao
            };
        }

        public List<ListarLogPatrimonioDTO> Listar()
        {
            List<LogPatrimonio> logs = _repository.Listar();
            if (logs == null)
                throw new DomainException("Patrimônio não encontrado");
            List<ListarLogPatrimonioDTO> listarDTO = logs.Select(varAux => lerDTO(varAux)).ToList();
            return listarDTO; 
        }

        public List<ListarLogPatrimonioDTO> BuscarPorPatrimonio(Guid patrimonioId)
        {
            List<LogPatrimonio> logs = _repository.BuscarPorPatrimonio(patrimonioId);
            if (logs == null)
                throw new DomainException("Patrimônio não encontrado");
            
            List<ListarLogPatrimonioDTO> listarDTO = logs.Select(varAux => lerDTO(varAux)).ToList();
            return listarDTO;
        }

    }
}
