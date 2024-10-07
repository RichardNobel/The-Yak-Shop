using YakShop.Server.Data.Repositories;

namespace YakShop.Server.Services
{
    internal class DailyHerdStatsUpdateService(
        ILogger<DailyHerdStatsUpdateService> logger,
        IProduceDayRepository produceDayRepo,
        IOrderRepository orderRepo
    )
    {
        private readonly ILogger<DailyHerdStatsUpdateService> _logger = logger;
        private readonly IProduceDayRepository _produceDayRepo = produceDayRepo;
        private readonly IOrderRepository _orderRepo = orderRepo;

        public async Task RunAsync()
        {
            await Task.Delay(100);
            _logger.LogInformation(
                "{ServiceName} did something.", nameof(DailyHerdStatsUpdateService));

            var latestProduceDayNumber = _produceDayRepo.GetLatest().DayNumber;

            var totalProduce = _produceDayRepo.GetTotalQuantitiesUntilDay(latestProduceDayNumber);
        }
    }
}