namespace Dida.Waylen.Onboarding.Demo.Service.Open.Applications.Hotels.Queries;

public record HotelPagedQuery(GetHotelPagedDto Dto) : Query<PagedResultDto<HotelDto>>
{
}
