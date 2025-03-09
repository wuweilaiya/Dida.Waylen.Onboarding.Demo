namespace Dida.Waylen.Onboarding.Demo.Service.Open.Applications.Hotels.Events;

public class HotelBatchDeletedEventHandler
{
    [LocalEventHandler]
    public Task HandleAsync(HotelBatchDeletedEvent @event)
    {
        // todo sync cahce

        return Task.CompletedTask;
    }
}
