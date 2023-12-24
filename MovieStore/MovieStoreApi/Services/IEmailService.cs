namespace MovieStore.Api.Services
{
    public interface IEmailService
    {
        void SendEmailAsync(string toEmail, string subject, string body);
    }
}
