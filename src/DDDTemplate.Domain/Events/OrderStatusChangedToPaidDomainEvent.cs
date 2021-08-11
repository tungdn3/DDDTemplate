using DDDTemplate.Domain.AggregatesModel.OrderAggregate;
using MediatR;
using System.Collections.Generic;

namespace DDDTemplate.Domain.Events
{
    public class OrderStatusChangedToPaidDomainEvent : INotification
    {
        public int OrderId { get; }

        public IEnumerable<OrderItem> OrderItems { get; }

        public OrderStatusChangedToPaidDomainEvent(int orderId, IEnumerable<OrderItem> orderItems)
        {
            OrderId = orderId;
            OrderItems = orderItems;
        }
    }
}
