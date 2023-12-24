using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStore.Core.Entities;
using MovieStore.Core.ValueObjects;

namespace MovieStore.Infrastructure.Configurations
{
    public class PurchasedMovieConfiguration : IEntityTypeConfiguration<PurchasedMovie>
    {
        public void Configure(EntityTypeBuilder<PurchasedMovie> builder)
        {
            builder.HasKey(pm => pm.Id);

            builder.HasOne(pm => pm.Movie)
                .WithMany() // Assuming there is no direct navigation from Movie to PurchasedMovie
                .IsRequired();

            builder.HasOne(pm => pm.Customer)
                .WithMany(c => c.PurchasedMovies)
                .IsRequired();

            builder.Property(m => m.Price)
                .HasConversion(
                    v => v.Value,
                    v => Money.Create(v).Value)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
