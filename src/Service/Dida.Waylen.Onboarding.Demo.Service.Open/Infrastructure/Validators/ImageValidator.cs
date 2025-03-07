namespace Dida.Waylen.Onboarding.Demo.Service.Open.Infrastructure.Validators;

/// <summary>
/// 图片详情DTO验证器
/// </summary>
public class ImageValidator : AbstractValidator<ImageDetailDto>
{
    public ImageValidator()
    {
        RuleFor(x => x.Url)
            .NotEmpty()
            .WithMessage("图片URL不能为空")
            .MaximumLength(1000)
            .WithMessage("图片URL长度不能超过1000个字符")
            .Must(BeValidUrl)
            .WithMessage("必须提供有效的URL地址")
            .Must(BeValidImageUrl)
            .WithMessage("URL必须指向有效的图片文件");

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .When(x => !string.IsNullOrEmpty(x.Description))
            .WithMessage("图片描述不能超过500个字符");
    }

    /// <summary>
    /// 验证URL是否有效
    /// </summary>
    private bool BeValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }

    /// <summary>
    /// 验证是否为图片URL
    /// </summary>
    private bool BeValidImageUrl(string url)
    {
        if (string.IsNullOrEmpty(url)) return false;

        var extension = Path.GetExtension(url).ToLower();
        var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
        return validExtensions.Contains(extension);
    }
}

/// <summary>
/// 图片DTO验证器
/// </summary>
public class ImageDtoValidator : AbstractValidator<ImageDto>
{
    public ImageDtoValidator()
    {
        RuleFor(x => x.Url)
            .NotEmpty()
            .WithMessage("主图URL不能为空")
            .MaximumLength(1000)
            .WithMessage("主图URL长度不能超过1000个字符")
            .Must(BeValidUrl)
            .WithMessage("必须提供有效的URL地址")
            .Must(BeValidImageUrl)
            .WithMessage("URL必须指向有效的图片文件");

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .When(x => !string.IsNullOrEmpty(x.Description))
            .WithMessage("主图描述不能超过500个字符");

        RuleFor(x => x.Attached)
            .NotNull()
            .WithMessage("附加图片集合不能为null");

        RuleForEach(x => x.Attached)
            .SetValidator(new ImageValidator())
            .When(x => x.Attached.Any());

        // 自定义复合验证规则
        RuleFor(x => x)
            .Must(HaveValidImageCount)
            .WithMessage("附加图片数量不能超过9张");

        RuleFor(x => x)
            .Must(HaveUniqueUrls)
            .WithMessage("所有图片URL必须唯一");
    }

    /// <summary>
    /// 验证URL是否有效
    /// </summary>
    private bool BeValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }

    /// <summary>
    /// 验证是否为图片URL
    /// </summary>
    private bool BeValidImageUrl(string url)
    {
        if (string.IsNullOrEmpty(url)) return false;

        var extension = Path.GetExtension(url).ToLower();
        var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
        return validExtensions.Contains(extension);
    }

    /// <summary>
    /// 验证附加图片数量
    /// </summary>
    private bool HaveValidImageCount(ImageDto image)
    {
        return image.Attached.Count <= 9;
    }

    /// <summary>
    /// 验证所有图片URL是否唯一
    /// </summary>
    private bool HaveUniqueUrls(ImageDto image)
    {
        var urls = new HashSet<string> { image.Url };
        return image.Attached.All(x => urls.Add(x.Url));
    }
}

