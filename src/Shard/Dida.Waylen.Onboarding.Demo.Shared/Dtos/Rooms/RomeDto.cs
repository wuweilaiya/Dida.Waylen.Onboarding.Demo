namespace Dida.Waylen.Onboarding.Demo.Shared.Model.Dtos.Rooms;

/// <summary>
/// 房间信息Dto
/// </summary>
public class RoomDto
{
    /// <summary>
    /// 房间Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 房间号码
    /// </summary>
    public string Number { get; set; } = string.Empty;

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

