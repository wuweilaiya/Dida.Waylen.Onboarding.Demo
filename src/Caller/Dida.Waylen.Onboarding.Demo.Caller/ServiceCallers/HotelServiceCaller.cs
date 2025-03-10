namespace Dida.Waylen.Onboarding.Demo.Caller.ServiceCallers;

public class HotelServiceCaller(HotelCallerClient callerClient) : ServiceCallerBase(callerClient), IScoped
{
    protected override string UrlPrefix => "api/v1/HotelServicecs";

    public async Task<PagedResultDto<HotelDto>> GetPagedAsync(GetHotelPagedDto dto) => await GetAsync<PagedResultDto<HotelDto>>(dto) ?? new();

    public Task<HotelDetailDto?> GetAsync(long id) => GetAsync<HotelDetailDto>($"{id}");

    public Task AddAsync(CreateHotelDto dto) => PostAsync(default, dto);

    public Task UpdateAsync(long id, UpdateHotelDto dto) => PutAsync($"{id}", dto);

    public Task DeleteAsync(long id) => DeleteAsync($"{id}");

    public Task BatchDeleteAsync(long[] ids) => DeleteAsync($"Batch?{string.Join("&", ids.Select(id => $"ids={id}"))}");

    public Task AddRoomAsync(long id, AddHotelRoomDto dto) => PostAsync($"{id}/Room", dto);

    public Task UpdateRoomAsync(long id, long roomId, UpdateHotelRoomDto dto) => PutAsync($"{id}/Room/{roomId}", dto);

    public Task DeleteRoomAsync(long id, long roomId) => DeleteAsync($"{id}/Room/{roomId}");

    public Task BatchDeleteRoomAsync(long id, long[] roomIds) => DeleteAsync($"Room/Batch?{string.Join("&", roomIds.Select(roomId => $"roomIds={roomId}"))}");
}
