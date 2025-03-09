using Dida.Waylen.Onboarding.Demo.Service.Open.Infrastructure.Events;

namespace Dida.Waylen.Onboarding.Demo.Service.Open.Applications.Hotels.Events;

public class HotelChangedEventHandler
{
    [LocalEventHandler]
    public Task HandleAsync(EntityChangedEvent<Hotel> @event)
    {
        // todo sync cahce

        return Task.CompletedTask;
    }
}
