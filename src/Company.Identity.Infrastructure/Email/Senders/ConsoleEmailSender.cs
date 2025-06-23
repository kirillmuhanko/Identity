using Company.Identity.Infrastructure.Email.Interfaces.Senders;

namespace Company.Identity.Infrastructure.Email.Senders;

public class ConsoleEmailSender : IEmailSender
{
    public Task SendAsync(string to, string subject, string body)
    {
        Console.WriteLine($"Sending email to {to}: {subject} - {body}");
        return Task.CompletedTask;
    }
}