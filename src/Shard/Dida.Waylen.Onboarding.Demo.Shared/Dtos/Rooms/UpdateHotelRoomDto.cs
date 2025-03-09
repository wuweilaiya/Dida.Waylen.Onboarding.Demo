namespace Dida.Waylen.Onboarding.Demo.Shared.Model.Dtos.Rooms;

/// <summary>
/// 更新房间请求Dto
/// </summary>
public class UpdateHotelRoomDto
{
    /// <summary>
    /// 房间号码
    /// </summary>
    public string? Number { get; set; }

    /// <summary>
    /// 房间类型
    /// </summary>
    public RoomTypeEnum? Type { get; set; }

    /// <summary>
    /// 房间类型描述
    /// </summary>
    public string? TypeDescription { get; set; }

    /// <summary>
    /// 床型
    /// </summary>
    public BedTypeEnum? BedType { get; set; }

    /// <summary>
    /// 床型描述
    /// </summary>
    public string? BedTypeDescription { get; set; }

    /// <summary>
    /// 房间描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 房间图片信息
    /// </summary>
    public ImageDto? Image { get; set; }
}
