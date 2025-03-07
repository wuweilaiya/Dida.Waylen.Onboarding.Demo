namespace Dida.Waylen.Onboarding.Demo.Service.Open.Data.ValueObject;

/// <summary>
/// 联系方式值对象，用于表示联系信息
/// </summary>
public class Contact : ValueObject
{
    /// <summary>
    /// 电话号码
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// 电子邮箱地址
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 网站地址
    /// </summary>
    public string Website { get; set; } = string.Empty;
}
