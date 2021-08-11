using DDDTemplate.Domain.AggregatesModel.OrderAggregate;
using MediatR;

namespace DDDTemplate.Domain.Events
{
    public class OrderCancelledDomainEvent : INotification
    {
        public Order Order { get; }

        public OrderCancelledDomainEvent(Order order)
        {
            Order = order;
        }
    }
}
