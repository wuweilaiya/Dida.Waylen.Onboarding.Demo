using Core.DataModel.Dtos;
using Dida.Waylen.Onboarding.Demo.Caller.ServiceCallers;
using Dida.Waylen.Onboarding.Demo.Contract.Interfaces;
using Dida.Waylen.Onboarding.Demo.Shared.Model.Dtos.Hotels;
using Dida.Waylen.Onboarding.Demo.Shared.Model.Dtos.Rooms;

namespace Dida.Waylen.Onboarding.Demo.Caller;

public class HotelCaller(HotelServiceCaller _hotelServiceCaller) : IHotelCaller
{
    public Task<HotelDetailDto> GetAsync(long id)
    {
        return _hotelServiceCaller.GetAsync(id);
    }

    public Task<PagedResultDto<HotelDto>> GetPagedAsync(GetHotelPagedDto dto)
    {
        return _hotelServiceCaller.GetPagedAsync(dto);
    }

    public Task AddAsync(CreateHotelDto dto)
    {
        throw new NotImplementedException();
    }

    public Task AddRoomAsync(long id, AddHotelRoomDto dto)
    {
        throw new NotImplementedException();
    }

    public Task BatchDeleteRoomAsync(long[] roomIds)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(long Id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRoomAsync(long id, long roomId)
    {
        throw new NotImplementedException();
    }

    public Task TaskBatchDeleteAsync(long[] ids)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(long id, UpdateHotelDto dto)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRoomAsync(long id, long roomId, UpdateHotelRoomDto dto)
    {
        throw new NotImplementedException();
    }
}
