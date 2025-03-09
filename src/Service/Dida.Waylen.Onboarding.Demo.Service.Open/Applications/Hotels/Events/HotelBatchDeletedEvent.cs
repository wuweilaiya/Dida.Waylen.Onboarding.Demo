namespace Dida.Waylen.Onboarding.Demo.Service.Open.Applications.Hotels.Events;

public record HotelBatchDeletedEvent(IEnumerable<Hotel> Hotels) : Event
{
}
