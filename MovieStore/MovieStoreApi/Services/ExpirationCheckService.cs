using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;
using MovieStore.Api.Services;
using MovieStore.Core.Entities;
using MovieStore.Infrastructure.Contracts;

public class ExpirationCheckService : IHostedService, IDisposable
{
    private readonly IEmailService _emailService;
    private readonly int _intervalMinutes;
    private readonly IServiceProvider _serviceProvider;
    private Timer? _timer;

    public ExpirationCheckService(
        IEmailService emailService,
        IServiceProvider serviceProvider,
        IOptions<BackgroundServiceOptions> options)
    {
        _emailService = emailService;
        _intervalMinutes = options.Value.ExpirationCheckIntervalMinutes;
        _serviceProvider = serviceProvider;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(CheckAndSendExpirationEmails, null, 0, _intervalMinutes);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
    private void CheckAndSendExpirationEmails(object? state)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var purcasedMovieRepository = scope.ServiceProvider.GetRequiredService<IRepository<Customer>>();

            var customers = purcasedMovieRepository.GetAll();

            foreach (var customer in customers)
            {
                foreach (var item in customer.PurchasedMovies.Where(pm => pm.MovieExpirationDate < DateTime.Now && pm.MovieExpirationDate >= DateTime.Now.AddMinutes(_intervalMinutes)))
                {
                    var emailSubject = "Movie Expiration Reminder";
                    var emailBody = $"Your license for the movie: {item.Movie!.Name.ToUpper()} is about to expire.\n Please renew your subscription.";

                    _emailService.SendEmailAsync(customer.Email.Value, emailSubject, emailBody);

                }
            }

        }
    }
}
