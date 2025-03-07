using System.ComponentModel.DataAnnotations.Schema;

namespace Dida.Waylen.Onboarding.Demo.Service.Open.Data.ValueObject;

/// <summary>
/// 图片值对象，用于管理主图和附加图片信息
/// </summary>
public class Image : ValueObject
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
    public List<ImageDetail> Attached { get; set; } = new List<ImageDetail>();

    /// <summary>
    /// 获取所有图片信息，包括主图和附加图
    /// </summary>
    /// <remarks>
    /// 使用 C# 12.0 集合表达式语法，将主图和附加图合并为一个列表
    /// </remarks>
    [NotMapped]
    public List<ImageDetail> All => [new ImageDetail { Url = Url, Description = Description }, .. Attached];
}

/// <summary>
/// 图片详情值对象，用于存储单个图片的信息
/// </summary>
public class ImageDetail
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
