using AssetControlAPI_.Interface;

namespace AssetControlAPI_.Applications.Services
{
    public class StatusPatrimonioService
    {
        private readonly IStatusPatrimonioRepository _repository;
        public StatusPatrimonioService(IStatusPatrimonioRepository repository) => _repository = repository; 
    }
}
