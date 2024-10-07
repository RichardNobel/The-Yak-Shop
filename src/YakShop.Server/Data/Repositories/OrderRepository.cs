using Microsoft.EntityFrameworkCore;
using YakShop.Server.Data.Entities;
using YakShop.Server.Models;

namespace YakShop.Server.Data.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Order CreateOrder(Order order);

        Task<(decimal milk, int skins)> GetTotalAmountsUntilDayAsync(int dayNumber);
    }

    public class OrderRepository(YakShopDbContext dbContext)
        : BaseRepository<Order>(dbContext),
            IOrderRepository
    {
        public Order CreateOrder(Order order)
        {
            if (string.IsNullOrEmpty(order.Customer?.Name))
            {
                throw new ArgumentException("Customer is required.");
            }

            var customerEntity =
                db.Customers.FirstOrDefault(c => c.Name == order.Customer.Name)
                ?? db.Customers.Add(new CustomerEntity(order.Customer.Name)).Entity;

            db.Orders.Add(new OrderEntity(order.DayNumber, customerEntity.Id, order.Milk, order.Skins));
            order.Customer = new Customer(customerEntity.Name) { Id = customerEntity.Id };
            return order;
        }

        public async Task<(decimal milk, int skins)> GetTotalAmountsUntilDayAsync(int dayNumber)
        {
            var totals = await db.Orders.Where(o => (o.DayNumber <= dayNumber))
                .GroupBy(pd => 1)
                .Select(g => new { Milk = g.Sum(o => o.Milk), Skins = g.Sum(o => o.Skins) })
                .FirstOrDefaultAsync();

            return totals == null ? (0, 0) : (totals.Milk, totals.Skins);
        }
    }
}