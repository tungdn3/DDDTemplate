namespace DDDTemplate.Domain.AggregatesModel.OrderAggregate
{
    public enum OrderStatus
    {
        Submitted,
        AwaitingValidation,
        StockConfirmed,
        Paid,
        Shipped,
        Cancelled,
    }
}
