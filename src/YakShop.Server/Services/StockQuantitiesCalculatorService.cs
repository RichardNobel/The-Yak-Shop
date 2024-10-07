using YakShop.Server.Data.Repositories;

namespace YakShop.Server.Services
{
    public class StockQuantitiesCalculatorService(
        IProduceDayRepository produceDayRepo,
        IOrderRepository orderRepo
    )
    {
        private readonly IProduceDayRepository _produceDayRepo = produceDayRepo;
        private readonly IOrderRepository _orderRepo = orderRepo;

        public async Task<(decimal milk, int skins)> CalculateForDayAsync(int dayNumber)
        {
            var produceTotals = await _produceDayRepo.GetTotalAmountsUntilDayAsync(dayNumber);
            var orderTotals = await _orderRepo.GetTotalAmountsUntilDayAsync(dayNumber);

            return (produceTotals.milk - orderTotals.milk, produceTotals.skins - orderTotals.skins);
        }
    }
}