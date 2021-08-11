using DDDTemplate.Domain.SeedWork;
using System.Threading.Tasks;

namespace DDDTemplate.Domain.AggregatesModel.OrderAggregate
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order Add(Order order);

        void Update(Order order);

        Task<Order> GetById(int orderId);
    }
}
