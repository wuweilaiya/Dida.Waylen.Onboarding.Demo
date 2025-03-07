namespace Dida.Waylen.Onboarding.Demo.Shared.Model.Dtos.Hotels;

/// <summary>
/// 更新酒店请求Dto
/// </summary>
public class UpdateHotelDto
{
    /// <summary>
    /// 酒店名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 酒店地址信息
    /// </summary>
    public AddressDto? Address { get; set; }

    /// <summary>
    /// 酒店联系方式
    /// </summary>
    public ContactDto? Contact { get; set; }

    /// <summary>
    /// 酒店图片信息
    /// </summary>
    public ImageDto? Image { get; set; }

    /// <summary>
    /// 酒店星级评定
    /// </summary>
    public HotelStarRatingEnum? HotelStarRating { get; set; }

    /// <summary>
    /// 酒店描述信息
    /// </summary>
    public string? Description { get; set; }
}
