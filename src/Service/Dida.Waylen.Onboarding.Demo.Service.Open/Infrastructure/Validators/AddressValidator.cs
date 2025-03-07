namespace Dida.Waylen.Onboarding.Demo.Service.Open.Infrastructure.Validators;

/// <summary>
/// 地址Dto验证器
/// </summary>
public class AddressValidator : AbstractValidator<AddressDto>
{
    public AddressValidator()
    {
        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90)
            .WithMessage("纬度必须在-90到90度之间");

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180)
            .WithMessage("经度必须在-180到180度之间");

        RuleFor(x => x.Country)
            .MaximumLength(50)
            .When(x => !string.IsNullOrEmpty(x.Country))
            .WithMessage("国家名称不能超过50个字符");

        RuleFor(x => x.CountryCode)
            .MaximumLength(10)
            .When(x => !string.IsNullOrEmpty(x.CountryCode))
            .WithMessage("国家代码不能超过10个字符");

        RuleFor(x => x.Province)
            .MaximumLength(50)
            .When(x => !string.IsNullOrEmpty(x.Province))
            .WithMessage("省份名称不能超过50个字符");

        RuleFor(x => x.ProvinceCode)
            .MaximumLength(10)
            .When(x => !string.IsNullOrEmpty(x.ProvinceCode))
            .WithMessage("省份代码不能超过10个字符");

        RuleFor(x => x.City)
            .MaximumLength(50)
            .When(x => !string.IsNullOrEmpty(x.City))
            .WithMessage("城市名称不能超过50个字符");

        RuleFor(x => x.CityCode)
            .MaximumLength(10)
            .When(x => !string.IsNullOrEmpty(x.CityCode))
            .WithMessage("城市代码不能超过10个字符");

        RuleFor(x => x.Region)
            .MaximumLength(50)
            .When(x => !string.IsNullOrEmpty(x.Region))
            .WithMessage("区域名称不能超过50个字符");

        RuleFor(x => x.RegionCode)
            .MaximumLength(10)
            .When(x => !string.IsNullOrEmpty(x.RegionCode))
            .WithMessage("区域代码不能超过10个字符");

        RuleFor(x => x.Street)
            .MaximumLength(100)
            .When(x => !string.IsNullOrEmpty(x.Street))
            .WithMessage("街道名称不能超过100个字符");

        RuleFor(x => x.Detail)
            .NotEmpty()
            .WithMessage("详细地址不能为空")
            .MaximumLength(500)
            .WithMessage("详细地址不能超过500个字符");

        RuleFor(x => x.PostalCode)
            .MaximumLength(10)
            .When(x => !string.IsNullOrEmpty(x.PostalCode))
            .WithMessage("邮政编码不能超过10个字符")
            .Matches(@"^\d*$")
            .When(x => !string.IsNullOrEmpty(x.PostalCode))
            .WithMessage("邮政编码只能包含数字");

        // 自定义复合验证规则
        RuleFor(x => x)
            .Must(HaveValidLocation)
            .WithMessage("必须提供有效的经纬度坐标");

        RuleFor(x => x)
            .Must(HaveValidAddressHierarchy)
            .WithMessage("地址层级必须完整（如果提供了下级地址，则上级地址不能为空）");
    }

    /// <summary>
    /// 验证经纬度是否有效
    /// </summary>
    private bool HaveValidLocation(AddressDto address)
    {
        // 如果经纬度都为0，认为是无效坐标
        return address.Latitude != 0 || address.Longitude != 0;
    }

    /// <summary>
    /// 验证地址层级的完整性
    /// </summary>
    private bool HaveValidAddressHierarchy(AddressDto address)
    {
        // 如果有城市，则必须有省份
        if (!string.IsNullOrEmpty(address.City) && string.IsNullOrEmpty(address.Province))
            return false;

        // 如果有区域，则必须有城市
        if (!string.IsNullOrEmpty(address.Region) && string.IsNullOrEmpty(address.City))
            return false;

        // 如果有街道，则必须有区域
        if (!string.IsNullOrEmpty(address.Street) && string.IsNullOrEmpty(address.Region))
            return false;

        return true;
    }
}
