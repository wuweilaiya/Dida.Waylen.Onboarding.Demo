namespace Dida.Waylen.Onboarding.Demo.Service.Open.Applications.Hotels.Commands;

public record DeleteHotelCommand(params long[] Ids) : Command
{
}
