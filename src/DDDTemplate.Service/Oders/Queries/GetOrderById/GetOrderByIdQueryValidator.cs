using FluentValidation;

namespace DDDTemplate.Service.Oders.Queries.GetOrderById
{
    public class GetOrderByIdQueryValidator : AbstractValidator<GetOrderByIdQuery>
    {
        public GetOrderByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
