using Core.DataModel.Dtos;
using Dida.Waylen.Onboarding.Demo.Shared.Model.Dtos.Hotels;
using Framework.Service.Caller.Abstractions;

namespace Dida.Waylen.Onboarding.Demo.Caller.ServiceCallers;

public class HotelServiceCaller : ServiceCallerBase
{
    private static readonly string _urlPrefix = "api/v1/HotelServicecs";

    public HotelServiceCaller(HotelCallerClient callerClient) : base(callerClient)
    {
    }

    public Task<PagedResultDto<HotelDto>> GetPagedAsync(GetHotelPagedDto dto)
    {
        return CallerClient.GetAsync<PagedResultDto<HotelDto>>($"{_urlPrefix}/Paged?{ToQueryString(dto)}");
    }

    public Task<HotelDetailDto> GetAsync(long id)
    {
        return CallerClient.GetAsync<HotelDetailDto>($"{_urlPrefix}/{id}");
    }

    string ToQueryString(GetHotelPagedDto dto)
    {
        var properties = dto.GetType().GetProperties();
        var queryString = string.Join("&", properties.Select(p => $"{p.Name}={p.GetValue(dto)}"));

        return queryString;
    }
}
