namespace Dida.Waylen.Onboarding.Demo.Service.Open.Applications.Hotels.Commands;

public record UpdateHotelRoomCommand(long HotelId,long RoomId, UpdateHotelRoomDto Dto) : Command
{
}
