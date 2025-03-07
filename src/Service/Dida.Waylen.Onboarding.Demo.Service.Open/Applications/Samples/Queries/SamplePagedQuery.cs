namespace Dida.Waylen.Onboarding.Demo.Service.Open.Applications.Samples.Queries
{
    public record SamplePagedQuery(GetPagedSampleRequestDto Dto) : Query<PagedResultDto<SampleItemDto>>
    {
    }
}
