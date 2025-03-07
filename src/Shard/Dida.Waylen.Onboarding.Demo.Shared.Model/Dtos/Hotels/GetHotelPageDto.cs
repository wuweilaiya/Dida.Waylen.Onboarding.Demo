namespace Dida.Waylen.Onboarding.Demo.Shared.Model.Dtos.Hotels;

/// <summary>
/// 酒店分页查询Dto
/// </summary>
public class GetHotelPageDto : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 搜索关键字（酒店名称/描述）
    /// </summary>
    public string? Search { get; set; }

    /// <summary>
    /// 酒店类型
    /// </summary>
    public HotelStarRatingEnum? HotelStarRating { get; set; }
}
