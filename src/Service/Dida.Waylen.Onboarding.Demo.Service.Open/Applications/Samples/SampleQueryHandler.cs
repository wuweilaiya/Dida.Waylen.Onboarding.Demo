namespace Dida.Waylen.Onboarding.Demo.Service.Open.Applications.Samples
{
    public class SampleQueryHandler(DemoDbContext _didaDbContext)
    {
        [LocalEventHandler]
        public async Task GetPagedListAsync(SamplePagedQuery query)
        {
            var queryable = _didaDbContext.Samples.AsNoTracking()
                .WhereIf(!string.IsNullOrEmpty(query.Dto.Search), e => e.Name.Contains(query.Dto.Search!));

            var paginationResult = await queryable.ToPaginationAsync(query.Dto.Page, query.Dto.PageSize, query.Dto.Sorting);

            query.Result = new PagedResultDto<SampleItemDto>(paginationResult.Total, paginationResult.Items.Adapt<List<SampleItemDto>>());
        }

        [LocalEventHandler]
        public async Task GetAsync(SampleDetailQuery query)
        {
            var sample = await _didaDbContext.Samples.AsNoTracking().FirstAsync(e => e.Id == query.Id);

            query.Result = sample.Adapt<SampleItemDto>();
        }
    }
}
