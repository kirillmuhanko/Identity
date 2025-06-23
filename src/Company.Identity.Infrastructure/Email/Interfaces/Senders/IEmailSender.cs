namespace Company.Identity.Infrastructure.Email.Interfaces.Senders;

public interface IEmailSender
{
    Task SendAsync(string to, string subject, string body);
}