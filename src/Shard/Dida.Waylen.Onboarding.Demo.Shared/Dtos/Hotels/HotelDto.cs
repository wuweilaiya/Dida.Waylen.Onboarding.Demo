namespace Dida.Waylen.Onboarding.Demo.Shared.Model.Dtos.Hotels;

/// <summary>
/// 酒店信息Dto
/// </summary>
public class HotelDto
{
    /// <summary>
    /// 酒店Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 酒店名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 酒店地址信息
    /// </summary>
    public AddressDto Address { get; set; } = new();

    /// <summary>
    /// 酒店联系方式
    /// </summary>
    public ContactDto Contact { get; set; } = new();

    /// <summary>
    /// 酒店图片信息
    /// </summary>
    public ImageDto Image { get; set; } = new();

    /// <summary>
    /// 酒店星级评定
    /// </summary>
    public HotelStarRatingEnum HotelStarRating { get; set; } = HotelStarRatingEnum.None;

    /// <summary>
    /// 酒店描述信息
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
