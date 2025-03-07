namespace Dida.Waylen.Onboarding.Demo.Shared.Model.Dtos;

/// <summary>
/// ͼƬ��ϢDto
/// </summary>
public class ImageDto
{
    /// <summary>
    /// ��ͼURL��ַ
    /// </summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// ��ͼ������Ϣ
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// ����ͼƬ����
    /// </summary>
    public List<ImageDetailDto> Attached { get; set; } = new();

    /// <summary>
    /// ����ͼƬ��Ϣ��������ͼ�͸���ͼ
    /// </summary>
    public List<ImageDetailDto> All => [new ImageDetailDto { Url = Url, Description = Description }, .. Attached];
}

/// <summary>
/// ͼƬ����Dto
/// </summary>
public class ImageDetailDto
{
    /// <summary>
    /// ͼƬURL��ַ
    /// </summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// ͼƬ������Ϣ
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
