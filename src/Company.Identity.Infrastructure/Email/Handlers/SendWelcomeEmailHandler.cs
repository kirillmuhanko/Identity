using Company.Identity.Application.Auth.Events;
using Company.Identity.Application.Event.Interfaces.Handlers;
using Company.Identity.Infrastructure.Email.Interfaces.Senders;

namespace Company.Identity.Infrastructure.Email.Handlers;

public class SendWelcomeEmailHandler(IEmailSender emailSender) : IEventHandler<UserCreatedEvent>
{
    public async Task HandleAsync(UserCreatedEvent user)
    {
        const string subject = "Welcome!";
        var body = $"Hi {user.UserName}, welcome to our platform!";
        await emailSender.SendAsync(user.Email, subject, body);
    }
}