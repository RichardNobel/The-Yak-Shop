using Microsoft.AspNetCore.SignalR;
using YakShop.Server.Data.Repositories;
using YakShop.Server.Models;

namespace YakShop.Server.Services
{
    internal class DailyHerdStatsUpdateService(
        IHubContext<RealTimeHub> realTimeHubContext,
        ILogger<DailyHerdStatsUpdateService> logger,
        IProduceDayRepository produceDayRepo,
        StockQuantitiesCalculatorService stockCalc
    )
    {
        private readonly ILogger<DailyHerdStatsUpdateService> _logger = logger;
        private readonly IProduceDayRepository _produceDayRepo = produceDayRepo;
        private readonly StockQuantitiesCalculatorService _stockCalc = stockCalc;

        public async Task RunAsync()
        {
            await Task.Delay(100);
            _logger.LogInformation(
                "{ServiceName} did something.", nameof(DailyHerdStatsUpdateService));

            var latestProduceDayNumber = _produceDayRepo.GetLatest().DayNumber;

            // Check stock amounts for milk & skins.
            var (milk, skins) = await _stockCalc.CalculateForDayAsync(latestProduceDayNumber);
            await realTimeHubContext.Clients.All.SendAsync("ReceiveStockData", new StockInfo(latestProduceDayNumber, milk, skins));


        }
    }
}