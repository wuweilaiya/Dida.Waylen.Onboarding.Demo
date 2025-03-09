namespace Dida.Waylen.Onboarding.Demo.Service.Open.Applications.Hotels.Commands;

public record AddHotelRoomCommand(long HotelId, AddHotelRoomDto Dto) : Command
{
}
