namespace Dida.Waylen.Onboarding.Demo.Service.Open.Dtos;

/// <summary>
/// 地址数据Dto
/// </summary>
public class AddressDto
{
    /// <summary>
    /// 纬度
    /// </summary>
    public int Latitude { get; set; }

    /// <summary>
    /// 经度
    /// </summary>
    public int Longitude { get; set; }

    /// <summary>
    /// 国家名称
    /// </summary>
    public string Country { get; set; } = string.Empty;

    /// <summary>
    /// 国家代码
    /// </summary>
    public string CountryCode { get; set; } = string.Empty;

    /// <summary>
    /// 省份名称
    /// </summary>
    public string Province { get; set; } = string.Empty;

    /// <summary>
    /// 省份代码
    /// </summary>
    public string ProvinceCode { get; set; } = string.Empty;

    /// <summary>
    /// 城市名称
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// 城市代码
    /// </summary>
    public string CityCode { get; set; } = string.Empty;

    /// <summary>
    /// 区域名称
    /// </summary>
    public string Region { get; set; } = string.Empty;

    /// <summary>
    /// 区域代码
    /// </summary>
    public string RegionCode { get; set; } = string.Empty;

    /// <summary>
    /// 街道名称
    /// </summary>
    public string Street { get; set; } = string.Empty;

    /// <summary>
    /// 详细地址
    /// </summary>
    public string Detail { get; set; } = string.Empty;

    /// <summary>
    /// 邮政编码
    /// </summary>
    public string PostalCode { get; set; } = string.Empty;
}
