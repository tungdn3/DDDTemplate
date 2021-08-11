using DDDTemplate.Domain.AggregatesModel.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDTemplate.Infrastructure.EntityConfigurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(o => o.Id)
                .UseHiLo("orderseq");

            builder.Ignore(x => x.DomainEvents);

            //Address value object persisted as owned entity type supported since EF Core 2.0
            builder.OwnsOne(
                x => x.Address,
                a =>
                {
                    a.Property<int>("OrderId")
                    .UseHiLo("orderseq");
                    a.WithOwner();
                });

            builder.Property<string>("Description")
                .IsRequired(false)
                .HasMaxLength(300);

            var orderItemsNavigation = builder.Metadata.FindNavigation(nameof(Order.OrderItems));
            orderItemsNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            //builder.HasOne<PaymentMethod>()
            //    .WithMany()
            //    // .HasForeignKey("PaymentMethodId")
            //    .HasForeignKey("_paymentMethodId")
            //    .IsRequired(false)
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne<Buyer>()
            //    .WithMany()
            //    .IsRequired(false)
            //    // .HasForeignKey("BuyerId");
            //    .HasForeignKey("_buyerId");

            //builder.HasOne(o => o.OrderStatus)
            //    .WithMany()
            //    // .HasForeignKey("OrderStatusId");
            //    .HasForeignKey("_orderStatusId");
        }
    }
}
