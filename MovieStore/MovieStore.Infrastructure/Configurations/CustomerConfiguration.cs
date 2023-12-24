using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStore.Core.Entities;
using MovieStore.Core.ValueObjects;

namespace MovieStore.Infrastructure.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Email)
                .HasConversion(
                    v => v.Value,
                    v => Email.Create(v).Value);

            builder.Property(e => e.MoneySpent)
                .HasConversion(
                    v => v.Value,
                    v => Money.Create(v).Value);

            builder.OwnsOne(
                c => c.Status,
                status =>
                {
                    status.Property(s => s.StatusValue).HasColumnName("Status");
                    status.Property(s => s.StatusExpirationDateValue)
                        .HasColumnName("StatusExpirationDate")
                        .HasConversion(
                            v => v.Value,
                            v => new ExpirationDate(v))
                        .IsRequired(false);
                });

            builder.HasMany(c => c.PurchasedMovies)
                .WithOne(pm => pm.Customer)
                .IsRequired();
        }
    }

}
