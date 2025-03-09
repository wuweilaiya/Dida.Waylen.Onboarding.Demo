namespace Dida.Waylen.Onboarding.Demo.Service.Open.Applications.Hotels.Commands;

public record UpdateHotelCommand(long Id, UpdateHotelDto Dto) : Command
{
}
