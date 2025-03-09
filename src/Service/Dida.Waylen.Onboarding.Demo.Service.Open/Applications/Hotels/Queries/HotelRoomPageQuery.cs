namespace Dida.Waylen.Onboarding.Demo.Service.Open.Applications.Hotels.Queries;

/// <summary>
/// 房间分页查询
/// </summary>
public record HotelRoomPageQuery(long HotelId, GetRoomPagedDto Dto) : Query<PagedResultDto<RoomDto>>
{
}
