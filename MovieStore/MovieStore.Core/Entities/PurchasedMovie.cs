using MovieStore.Core.ValueObjects;

namespace MovieStore.Core.Entities;

public class PurchasedMovie
{
    public PurchasedMovie(Movie movie, Customer customer, DateTime dateOfPurchase, DateTime? expirationDate, Money price)
    {
        Movie = movie;
        Customer = customer;
        DateOfPurchase = dateOfPurchase;
        MovieExpirationDate = expirationDate;
        Price = price;
    }
    private PurchasedMovie() { }

    public Guid Id { get; private set; }
    public Movie? Movie { get; private set; }
    public Customer? Customer { get; private set; } 
    public DateTime DateOfPurchase { get; private set; }
    public DateTime? MovieExpirationDate { get; private set; }
    public Money? Price { get; private set; }

}
