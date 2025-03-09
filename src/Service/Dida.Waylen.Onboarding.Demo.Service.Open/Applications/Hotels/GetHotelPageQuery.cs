namespace Dida.Waylen.Onboarding.Demo.Service.Open.Applications.Hotels;

/// <summary>
/// 酒店分页查询
/// </summary>
public record GetHotelPageQuery(GetRoomPagedDto Dto) : Query<PagedResultDto<RoomDto>>
{
}

