using DDDTemplate.Domain.SeedWork;
using MediatR;
using System.Linq;
using System.Threading.Tasks;

namespace DDDTemplate.Infrastructure.Extensions
{
    internal static class MediatorExtension
    {
        public static async Task DispatchDomainEvents(this IMediator mediator, DDDTemplateContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }

            domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());
        }
    }
}
