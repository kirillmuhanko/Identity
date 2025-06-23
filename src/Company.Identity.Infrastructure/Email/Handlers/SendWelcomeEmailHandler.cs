using Company.Identity.Application.Event.Interfaces.Handlers;
using Company.Identity.Application.User.Events;
using Company.Identity.Infrastructure.Email.Interfaces.Senders;

namespace Company.Identity.Infrastructure.Email.Handlers;

public class SendWelcomeEmailHandler(IEmailSender emailSender) : IEventHandler<UserCreatedEvent>
{
    public async Task HandleAsync(UserCreatedEvent @event)
    {
        const string subject = "Welcome!";
        var body = $"Hi {@event.UserName}, welcome to our platform!";
        await emailSender.SendAsync(@event.Email, subject, body);
    }
}