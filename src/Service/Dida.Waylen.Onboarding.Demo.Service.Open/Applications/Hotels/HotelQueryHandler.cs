using Dida.Waylen.Onboarding.Demo.Service.Open.Applications.Hotels.Queries;

namespace Dida.Waylen.Onboarding.Demo.Service.Open.Applications.Hotels;

public class HotelQueryHandler(DemoDbContext _didaDbContext)
{
    [LocalEventHandler]
    public async Task GetPagedListAsync(HotelPagedQuery query)
    {
        var queryable = _didaDbContext.Hotels.AsNoTracking()
                                             .WhereIf(!string.IsNullOrEmpty(query.Dto.Search), e => e.Name.Contains(query.Dto.Search!))
                                             .WhereIf(query.Dto.HotelStarRating is not null, e => e.HotelStarRating == query.Dto.HotelStarRating);
        var paginationResult = await queryable.ToPaginationAsync(query.Dto.Page, query.Dto.PageSize, query.Dto.Sorting);
        query.Result = new PagedResultDto<HotelDto>(paginationResult.Total, paginationResult.Items.Adapt<List<HotelDto>>());
    }

    [LocalEventHandler]
    public async Task GetAsync(HotelDetailQuery query)
    {
        var hotel = await _didaDbContext.Hotels.AsNoTracking()
                                               .Include(x => x.Rooms)
                                               .FirstAsync(e => e.Id == query.Id);
        query.Result = hotel.Adapt<HotelDetailDto>();
    }

    [LocalEventHandler]
    public async Task GetRoomPagedAsync(HotelRoomPageQuery query)
    {
        var queryable = _didaDbContext.Rooms.AsNoTracking()
                                             .Where(e => e.Hotel.Id == query.HotelId)
                                             .WhereIf(!string.IsNullOrEmpty(query.Dto.Search), e => e.Number.Contains(query.Dto.Search!))
                                             .WhereIf(query.Dto.Type is not null, e => e.Type == query.Dto.Type)
                                             .WhereIf(query.Dto.BedType is not null, e => e.BedType == query.Dto.BedType);
        var paginationResult = await queryable.ToPaginationAsync(query.Dto.Page, query.Dto.PageSize, query.Dto.Sorting);
        query.Result = new PagedResultDto<RoomDto>(paginationResult.Total, paginationResult.Items.Adapt<List<RoomDto>>());
    }
}
