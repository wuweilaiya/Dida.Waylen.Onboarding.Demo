namespace Dida.Waylen.Onboarding.Demo.Service.Open.Data.Entities;

/// <summary>
/// 酒店实体类，表示酒店的基本信息及其包含的房间
/// </summary>
public class Hotel : FullAuditedEntity<long>
{
    /// <summary>
    /// 酒店名称
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// 酒店地址信息
    /// </summary>
    public required Address Address { get; set; }

    /// <summary>
    /// 酒店联系方式
    /// </summary>
    public required Contact Contact { get; set; }

    /// <summary>
    /// 酒店图片信息
    /// </summary>
    public Image Image { get; set; } = new();

    /// <summary>
    /// 酒店星级评定
    /// </summary>
    public HotelStarRatingEnum HotelStarRating { get; set; } = HotelStarRatingEnum.None;

    /// <summary>
    /// 酒店描述信息
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 酒店包含的房间列表
    /// </summary>
    private List<Room> _rooms = new();

    /// <summary>
    /// 只读的房间集合，供外部访问
    /// </summary>
    public IReadOnlyCollection<Room> Rooms => _rooms;

    /// <summary>
    /// 私有构造函数，用于 EF Core 和序列化
    /// </summary>
    private Hotel()
    {
    }

    /// <summary>
    /// 创建酒店实体
    /// </summary>
    /// <param name="name">酒店名称</param>
    /// <param name="address">酒店地址</param>
    /// <param name="contact">联系方式</param>
    public Hotel(string name, Address address, Contact contact)
    {
        Name = name;
        Address = address;
        Contact = contact;
    }

    /// <summary>
    /// 添加房间到酒店
    /// </summary>
    /// <param name="rooms">要添加的房间数组</param>
    public void AddRooms(params Room[] rooms)
    {
        _rooms.AddRange(rooms);
    }

    /// <summary>
    /// 从酒店移除指定房间
    /// </summary>
    /// <param name="rooms">要移除的房间数组</param>
    public void RemoveRooms(params Room[] rooms)
    {
        _rooms.RemoveAll(rooms.Contains);
    }
}
