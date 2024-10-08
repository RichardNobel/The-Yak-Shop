using Microsoft.AspNetCore.SignalR;
using YakShop.Server.Data.Repositories;
using YakShop.Server.Helpers;
using YakShop.Server.Models;

namespace YakShop.Server.Services
{
    internal class DailyHerdStatsUpdateService(
        IHerdRepository herdRepo,
        IHubContext<RealTimeHub> realTimeHubContext,
        ILogger<DailyHerdStatsUpdateService> logger,
        IProduceDayRepository produceDayRepo,
        StockQuantitiesCalculatorService stockCalc
    )
    {
        private readonly IHerdRepository herdRepo = herdRepo;
        private readonly ILogger<DailyHerdStatsUpdateService> logger = logger;
        private readonly IProduceDayRepository produceDayRepo = produceDayRepo;
        private readonly StockQuantitiesCalculatorService stockCalc = stockCalc;

        public async Task RunAsync()
        {
            logger.LogInformation(
                "{ServiceName} did something.", nameof(DailyHerdStatsUpdateService));

            var latestProduceDayNumber = produceDayRepo.GetLatest().DayNumber;

            // Check stock amounts for milk & skins.
            var (stockMilk, stockSkins) = await stockCalc.CalculateForDayAsync(latestProduceDayNumber);
            await realTimeHubContext.Clients.All.SendAsync("ReceiveStockData", new StockInfo(latestProduceDayNumber, stockMilk, stockSkins));

            // Update age and shave status for each member of the herd.
            decimal milk = 0;
            int skins = 0;

            var herd = herdRepo.GetHerd();
            foreach (var yak in herd.Members)
            {
                if (yak.Age >= 10)
                {
                    // R.I.P. yak ;-(
                    continue;
                }

                yak.Age += (decimal)0.01; // 1 day older

                if (YakProduceCalculator.IsEligibleToBeShaved(yak.AgeInDays, yak.AgeNextShave * 100))
                {
                    skins++;
                    yak.AgeLastShaved = yak.Age;
                    yak.AgeNextShave = YakProduceCalculator.NextShaveDay(yak.AgeInDays);
                }

                herdRepo.UpdateMember(yak);

                if (yak.Sex.Equals("FEMALE", StringComparison.InvariantCultureIgnoreCase))
                {
                    milk += YakProduceCalculator.SingleYakLitersOfMilkByAge(yak.AgeInDays);
                }
            }

            produceDayRepo.Add(new ProduceDay(latestProduceDayNumber + 1, milk, skins));
            produceDayRepo.Save();
        }
    }
}