using MovieStore.Core.ValueObjects;
using System.Data;

namespace MovieStore.Core.Entities;

public class Customer
{
    public Guid Id { get; private set; }
    public Email Email { get; private set; }
    public CustomerStatus Status { get; private set; }
    public Role Role { get; private set; }
    private IList<PurchasedMovie> _purchasedMovies { get; set; } = new List<PurchasedMovie>();
    public IReadOnlyList<PurchasedMovie> PurchasedMovies => _purchasedMovies.AsReadOnly();
    public Money MoneySpent { get; private set; }

    public Customer(Email email)
    {
        Email = email;
        Role = Role.Regular;
        Status = CustomerStatus.Regular;
        _purchasedMovies = new List<PurchasedMovie>();
        MoneySpent = Money.Create(0).Value;
    }

    public void PurchaseMovie(Movie movie)
    {
        if(_purchasedMovies.FirstOrDefault(pm => pm.Movie == movie) is null)
        {
            var modifier = Status.GetModifier();

            var expirationDate = movie.GetExpirationDate();
            var price = movie.GetPrice(modifier);

            var purchasedMovie = new PurchasedMovie(movie, this, DateTime.Now, expirationDate, price);

            _purchasedMovies.Add(purchasedMovie);

            MoneySpent = Money.Create(MoneySpent.Value + purchasedMovie.Price.Value).Value;
        }
    }

    public void PromoteCustomer() => Status = new CustomerStatus(Core.Status.Advanced, new ExpirationDate(DateTime.Now.AddYears(1)));
    public void UpdateCustomer(Email email, Role role)
    {
        Email = email;
        Role = role;
    }


}
