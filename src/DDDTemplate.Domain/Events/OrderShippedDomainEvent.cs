using DDDTemplate.Domain.AggregatesModel.OrderAggregate;
using MediatR;

namespace DDDTemplate.Domain.Events
{
    public class OrderShippedDomainEvent : INotification
    {
        public Order Order { get; }

        public OrderShippedDomainEvent(Order order)
        {
            Order = order;
        }
    }
}
