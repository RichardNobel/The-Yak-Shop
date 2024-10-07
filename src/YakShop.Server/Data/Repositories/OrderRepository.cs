using YakShop.Server.Data.Entities;
using YakShop.Server.Models;

namespace YakShop.Server.Data.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Order CreateOrder(Order order);

        (decimal milk, int skins) GetTotalQuantitiesUntilDay(int dayNumber);
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

        public (decimal milk, int skins) GetTotalQuantitiesUntilDay(int dayNumber)
        {
            var totals = db.Orders.Where(o => (o.DayNumber <= dayNumber))
                .GroupBy(pd => 1)
                .Select(g => new { Milk = g.Sum(o => o.Milk), Skins = g.Sum(o => o.Skins) })
                .FirstOrDefault();
            return totals == null ? (0, 0) : (totals.Milk, totals.Skins);
        }
    }
}