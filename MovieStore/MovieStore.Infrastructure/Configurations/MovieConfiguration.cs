using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStore.Core;
using MovieStore.Core.Entities;
using MovieStore.Core.ValueObjects;
using System.Reflection.Emit;

namespace MovieStore.Infrastructure.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(m => m.Id);
            builder.ToTable("Movies")
            .HasDiscriminator<LicensingTypes>("LicensingType")
            .HasValue<LifeLongMovie>(LicensingTypes.LifeLong)
            .HasValue<TwoDayMovie>(LicensingTypes.TwoDay);
            builder.Property(m => m.Name);
            builder.Property(m => m.DateOfRelease);
            builder.Property(m => m.Price)
            .HasConversion(
                v => v.Value,
                v => Money.Create(v).Value
                )
            .HasColumnType("decimal(18, 2)");
            builder.Property(m => m.LicensingType);
        }
    }
}
