using MovieStore.Core.ValueObjects;

namespace MovieStore.Core.Entities;

public abstract class Movie
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime? DateOfRelease { get; set; }
    public virtual LicensingTypes LicensingType { get; set; }
    public Money? Price { get; set; }
    public abstract DateTime? GetExpirationDate();
    public virtual Money GetPrice(decimal modifier)
    {
        return Money.Create(GetBasePrice().Value * modifier).Value;
    }

    public abstract Money GetBasePrice();

}
