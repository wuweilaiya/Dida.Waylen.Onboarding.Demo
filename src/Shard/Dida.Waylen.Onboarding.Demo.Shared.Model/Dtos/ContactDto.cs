namespace Dida.Waylen.Onboarding.Demo.Shared.Model.Dtos;

/// <summary>
/// 联系方式Dto
/// </summary>
public class ContactDto
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
