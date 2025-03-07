namespace Dida.Waylen.Onboarding.Demo.Service.Open.Infrastructure.Validators;

/// <summary>
/// 创建酒店DTO验证器
/// </summary>
public class CreateHotelValidator : AbstractValidator<CreateHotelDto>
{
    public CreateHotelValidator(
        IValidator<AddressDto> addressValidator,
        IValidator<ContactDto> contactValidator,
        IValidator<ImageDto> imageValidator)
    {
        // 酒店名称验证
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("酒店名称不能为空")
            .MaximumLength(100)
            .WithMessage("酒店名称不能超过100个字符")
            .MinimumLength(2)
            .WithMessage("酒店名称不能少于2个字符");

        // 酒店地址验证
        RuleFor(x => x.Address)
            .NotNull()
            .WithMessage("酒店地址信息不能为空")
            .SetValidator(addressValidator);

        // 联系方式验证
        RuleFor(x => x.Contact)
            .NotNull()
            .WithMessage("酒店联系方式不能为空")
            .SetValidator(contactValidator);

        // 图片信息验证
        RuleFor(x => x.Image)
            .SetValidator(imageValidator)
            .When(x => x.Image != null);

        // 酒店星级验证
        RuleFor(x => x.HotelStarRating)
            .IsInEnum()
            .WithMessage("请选择有效的酒店星级")
            .NotEqual(HotelStarRatingEnum.None)
            .WithMessage("请选择酒店星级");

        // 描述信息验证
        RuleFor(x => x.Description)
            .MaximumLength(2000)
            .WithMessage("酒店描述不能超过2000个字符")
            .Must(BeValidDescription)
            .When(x => !string.IsNullOrEmpty(x.Description))
            .WithMessage("酒店描述包含无效字符");

        // 复合验证规则
        RuleFor(x => x)
            .Must(BeValidHotelData)
            .WithMessage("酒店数据验证失败");
    }

    /// <summary>
    /// 验证描述信息是否包含有效字符
    /// </summary>
    private bool BeValidDescription(string description)
    {
        if (string.IsNullOrEmpty(description)) return true;

        // 检查是否包含特殊字符或HTML标签
        var invalidChars = new[] { '<', '>', '&', '"', '\'' };
        return !description.Any(c => invalidChars.Contains(c));
    }

    /// <summary>
    /// 验证酒店数据的整体有效性
    /// </summary>
    private bool BeValidHotelData(CreateHotelDto hotel)
    {
        // 如果是高星级酒店（4星及以上），必须提供图片
        if (hotel.HotelStarRating >= HotelStarRatingEnum.FourStar)
        {
            if (string.IsNullOrEmpty(hotel.Image?.Url))
            {
                return false;
            }
        }

        // 其他复合验证规则...
        return true;
    }
}

