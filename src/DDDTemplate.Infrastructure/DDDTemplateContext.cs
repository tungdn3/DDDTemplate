using DDDTemplate.Domain.AggregatesModel.OrderAggregate;
using DDDTemplate.Domain.SeedWork;
using DDDTemplate.Infrastructure.EntityConfigurations;
using DDDTemplate.Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DDDTemplate.Infrastructure
{
    public class DDDTemplateContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public DDDTemplateContext(DbContextOptions<DDDTemplateContext> options, IMediator mediator)
            : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public async Task<bool> SaveChanges(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEvents(this);

            await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new OrderConfiguration());
        }
    }
}
