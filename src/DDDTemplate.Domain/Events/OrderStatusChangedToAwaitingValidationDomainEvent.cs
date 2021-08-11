using DDDTemplate.Domain.AggregatesModel.OrderAggregate;
using MediatR;
using System.Collections.Generic;

namespace DDDTemplate.Domain.Events
{
    public class OrderStatusChangedToAwaitingValidationDomainEvent : INotification
    {
        public int OrderId { get; }
        
        public IEnumerable<OrderItem> OrderItems { get; }

        public OrderStatusChangedToAwaitingValidationDomainEvent(int orderId, IEnumerable<OrderItem> orderItems)
        {
            OrderId = orderId;
            OrderItems = orderItems;
        }
    }
}
