using System.Net.Mail;
using System.Net;
using MovieStore.Api.Services;

public class GmailEmailService : IEmailService
{
    private readonly string _senderEmail= "blagojejevrosimov23@gmail.com";
    private readonly string _senderPassword = "khdx rofc knwi ikin";

    public void SendEmailAsync(string toEmail, string subject, string body)
    {
        using (var client = new SmtpClient("smtp.gmail.com"))
        {
            client.Port = 587;
            client.Credentials = new NetworkCredential(_senderEmail, _senderPassword);
            client.EnableSsl = true;

            var message = new MailMessage(_senderEmail, toEmail, subject, body);
            message.IsBodyHtml = true;

            client.Send(message);
        }
    }
}