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

        public (decimal milk, int skins) CalculateForDay(int dayNumber)
        {
            var produceTotals = _produceDayRepo.GetTotalQuantitiesUntilDay(dayNumber);
            var orderTotals = _orderRepo.GetTotalQuantitiesUntilDay(dayNumber);

            return (produceTotals.milk - orderTotals.milk, produceTotals.skins - orderTotals.skins);
        }
    }
}