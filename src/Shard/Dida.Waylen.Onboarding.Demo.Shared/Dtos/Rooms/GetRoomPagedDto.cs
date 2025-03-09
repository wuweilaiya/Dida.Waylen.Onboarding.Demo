namespace Dida.Waylen.Onboarding.Demo.Shared.Model.Dtos.Rooms;

/// <summary>
/// 房间分页查询Dto
/// </summary>
public class GetRoomPagedDto : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 搜索关键字（房间号码/描述）
    /// </summary>
    public string? Search { get; set; }

    /// <summary>
    /// 房间类型
    /// </summary>
    public RoomTypeEnum? Type { get; set; }

    /// <summary>
    /// 床型
    /// </summary>
    public BedTypeEnum? BedType { get; set; }
}
