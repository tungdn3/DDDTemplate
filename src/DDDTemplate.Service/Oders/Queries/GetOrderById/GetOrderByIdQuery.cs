using MediatR;

namespace DDDTemplate.Service.Oders.Queries.GetOrderById
{
    public class GetOrderByIdQuery : IRequest<OrderViewModel>
    {
        public int Id { get; set; }
    }
}
