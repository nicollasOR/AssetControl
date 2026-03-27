using AssetControlAPI_.Domains;
using AssetControlAPI_.Interface;
using AssetControlAPI_.Applications.DTOs.CidadeDTO;

namespace AssetControlAPI_.Applications.Services
{
    public class CidadeService
    {

        private readonly ICidadeRepository _repository;
        public CidadeService(ICidadeRepository repository) => _repository = repository; 

        private static ListarCidadeDTO ListarDTO(Cidade cidade)
        {
            ListarCidadeDTO ListarDTO = new ListarCidadeDTO()
            {
               
            }
        }

    }
}
