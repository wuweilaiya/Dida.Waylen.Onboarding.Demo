namespace Dida.Waylen.Onboarding.Demo.Shared.Model.Dtos.Rooms;

/// <summary>
/// 创建房间请求Dto
/// </summary>
public class CreateRoomDto
{
    /// <summary>
    /// 房间号码
    /// </summary>
    public required string Number { get; set; }

    /// <summary>
    /// 房间类型
    /// </summary>
    public RoomTypeEnum Type { get; set; } = RoomTypeEnum.None;

    /// <summary>
    /// 房间类型描述
    /// </summary>
    public string TypeDescription { get; set; } = string.Empty;

    /// <summary>
    /// 床型
    /// </summary>
    public BedTypeEnum BedType { get; set; } = BedTypeEnum.None;

    /// <summary>
    /// 床型描述
    /// </summary>
    public string BedTypeDescription { get; set; } = string.Empty;

    /// <summary>
    /// 房间描述
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 房间图片信息
    /// </summary>
    public ImageDto Image { get; set; } = new();
}
