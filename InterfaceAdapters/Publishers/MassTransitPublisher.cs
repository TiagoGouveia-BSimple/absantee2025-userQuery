using Domain.Models;
using InterfaceAdapters.Messages;
using MassTransit;

public class MassTransitPublisher : IMessagePublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MassTransitPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishCreatedUserMessageAsync(Guid id, string names, string surnames, string email, PeriodDateTime periodDateTime)
    {
        await _publishEndpoint.Publish(new UserCreatedMessage(id, names, surnames, email, periodDateTime));
    }
}
