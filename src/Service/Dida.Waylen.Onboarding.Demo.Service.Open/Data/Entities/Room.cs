namespace Dida.Waylen.Onboarding.Demo.Service.Open.Data.Entities;

/// <summary>
/// 房间实体，表示酒店内的具体房间信息
/// </summary>
public class Room : FullAuditedEntity<long>
{
    /// <summary>
    /// 所属酒店
    /// </summary>
    public Hotel Hotel { get; set; } = default!;

    /// <summary>
    /// 房间号码
    /// </summary>
    public required string Number { get; set; } = string.Empty;

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
    public Image Image { get; set; } = new();
}
