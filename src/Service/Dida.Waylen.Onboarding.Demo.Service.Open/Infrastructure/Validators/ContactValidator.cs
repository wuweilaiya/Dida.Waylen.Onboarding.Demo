namespace Dida.Waylen.Onboarding.Demo.Service.Open.Infrastructure.Validators;

/// <summary>
/// 联系方式DTO验证器
/// </summary>
public class ContactValidator : AbstractValidator<ContactDto>
{
    public ContactValidator()
    {
        // 电话号码验证
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("电话号码不能为空")
            .MaximumLength(20)
            .WithMessage("电话号码不能超过20个字符")
            .Must(BeValidPhoneNumber)
            .WithMessage("请输入有效的电话号码格式");

        // 电子邮箱验证
        RuleFor(x => x.Email)
            .MaximumLength(100)
            .WithMessage("邮箱地址不能超过100个字符")
            .EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.Email))
            .WithMessage("请输入有效的电子邮箱地址");

        // 网站地址验证
        RuleFor(x => x.Website)
            .MaximumLength(500)
            .WithMessage("网站地址不能超过500个字符")
            .Must(BeValidUrl)
            .When(x => !string.IsNullOrEmpty(x.Website))
            .WithMessage("请输入有效的网站地址");

        // 至少提供一种联系方式
        RuleFor(x => x)
            .Must(HaveAtLeastOneContactMethod)
            .WithMessage("至少需要提供一种有效的联系方式（电话、邮箱或网站）");
    }

    /// <summary>
    /// 验证电话号码格式
    /// </summary>
    private bool BeValidPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrEmpty(phoneNumber)) return false;

        // 支持以下格式：
        // - 手机号：1开头的11位数字
        // - 座机号：区号(3-4位)-号码(7-8位)
        // - 国际号码：+国家代码-号码
        var phonePatterns = new[]
        {
            @"^1[3-9]\d{9}$",                     // 手机号
            @"^0\d{2,3}-\d{7,8}$",               // 座机号
            @"^\+\d{1,4}-\d{6,20}$"              // 国际号码
        };

        return phonePatterns.Any(pattern => System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, pattern));
    }

    /// <summary>
    /// 验证URL是否有效
    /// </summary>
    private bool BeValidUrl(string url)
    {
        if (string.IsNullOrEmpty(url)) return true; // 允许为空

        // 检查是否是有效的HTTP/HTTPS URL
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }

    /// <summary>
    /// 验证是否至少提供了一种有效的联系方式
    /// </summary>
    private bool HaveAtLeastOneContactMethod(ContactDto contact)
    {
        return !string.IsNullOrWhiteSpace(contact.PhoneNumber) 
            || !string.IsNullOrWhiteSpace(contact.Email)
            || !string.IsNullOrWhiteSpace(contact.Website);
    }
}

