namespace Dida.Waylen.Onboarding.Demo.Service.Open.Applications.Rooms;

/// <summary>
/// 房间分页查询
/// </summary>
public record GetRoomPageQuery(GetRoomPageDto Dto) : Query<PagedResultDto<RoomDto>>
{
}
