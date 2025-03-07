using System.ComponentModel;

namespace Dida.Waylen.Onboarding.Demo.Shared.Model.Enums;

/// <summary>
/// 酒店星级
/// </summary>
public enum HotelStarRatingEnum
{
    None = 0,
    /// <summary>
    /// 一星
    /// </summary>
    [Description("一星")]
    OneStar = 1,
    /// <summary>
    /// 一星
    /// </summary>
    [Description("二星")]
    TwoStar = 2,
    /// <summary>
    /// 一星
    /// </summary>
    [Description("三星")]
    ThreeStar = 3,
    /// <summary>
    /// 一星
    /// </summary>
    [Description("四星")]
    FourStar = 4,
    /// <summary>
    /// 一星
    /// </summary>
    [Description("五星")]
    FiveStar = 5
}
