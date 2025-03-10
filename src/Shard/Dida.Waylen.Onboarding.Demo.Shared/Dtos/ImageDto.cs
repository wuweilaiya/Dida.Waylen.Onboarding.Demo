namespace Dida.Waylen.Onboarding.Demo.Shared.Model.Dtos;

/// <summary>
/// 图片信息Dto
/// </summary>
public class ImageDto
{
    /// <summary>
    /// 主图URL地址
    /// </summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// 主图描述信息
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 附加图片集合
    /// </summary>
    public List<ImageAttachedDto> Attached { get; set; } = new();

    /// <summary>
    /// 所有图片信息，包括主图和附加图
    /// </summary>
    public List<ImageAttachedDto> All => [new ImageAttachedDto { Url = Url, Description = Description }, .. Attached];
}

/// <summary>
/// 图片详情Dto
/// </summary>
public class ImageAttachedDto
{
    /// <summary>
    /// 图片URL地址
    /// </summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// 图片描述信息
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
