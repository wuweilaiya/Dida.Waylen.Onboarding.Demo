namespace Dida.Waylen.Onboarding.Demo.Service.Open.Applications.Hotels.Commands;

public record RemoveHotelRoomCommand(long HotelId, params long[] RoomIds) : Command
{
}
