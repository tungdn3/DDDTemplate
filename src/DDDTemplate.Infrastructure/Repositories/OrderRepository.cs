using DDDTemplate.Domain.AggregatesModel.OrderAggregate;
using DDDTemplate.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DDDTemplate.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DDDTemplateContext _context;

        public OrderRepository(DDDTemplateContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public Order Add(Order order)
        {
            return _context.Orders.Add(order).Entity;
        }

        public async Task<Order> GetById(int orderId)
        {
            var order = await _context
                .Orders
                .Include(x => x.Address)
                .FirstOrDefaultAsync(x => x.Id == orderId);

            if (order != null)
            {
                await _context.Entry(order)
                    .Collection(x => x.OrderItems)
                    .LoadAsync();
            }

            return order;
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);
        }
    }
}
