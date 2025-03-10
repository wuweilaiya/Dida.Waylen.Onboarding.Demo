using Core.DataModel.Dtos;
using Dida.Waylen.Onboarding.Demo.Caller.ServiceCallers;
using Dida.Waylen.Onboarding.Demo.Contract.Interfaces;
using Dida.Waylen.Onboarding.Demo.Shared.Model.Dtos.Hotels;
using Dida.Waylen.Onboarding.Demo.Shared.Model.Dtos.Rooms;

namespace Dida.Waylen.Onboarding.Demo.Caller;

/// <summary>
/// 酒店服务调用者
/// </summary>
public class HotelCaller(HotelServiceCaller _hotelServiceCaller) : IHotelCaller
{
    /// <summary>
    /// 获取酒店详细信息
    /// </summary>
    public Task<HotelDetailDto?> GetAsync(long id) => _hotelServiceCaller.GetAsync(id);

    /// <summary>
    /// 获取酒店分页列表
    /// </summary>
    public Task<PagedResultDto<HotelDto>> GetPagedAsync(GetHotelPagedDto dto)
        => _hotelServiceCaller.GetPagedAsync(dto);

    /// <summary>
    /// 添加新酒店
    /// </summary>
    public Task AddAsync(CreateHotelDto dto)
        => _hotelServiceCaller.AddAsync(dto);

    /// <summary>
    /// 更新酒店信息
    /// </summary>
    public Task UpdateAsync(long id, UpdateHotelDto dto)
        => _hotelServiceCaller.UpdateAsync(id, dto);

    /// <summary>
    /// 删除酒店
    /// </summary>
    public Task DeleteAsync(long Id)
        => _hotelServiceCaller.DeleteAsync(Id);

    /// <summary>
    /// 批量删除酒店
    /// </summary>
    public Task TaskBatchDeleteAsync(long[] ids)
        => _hotelServiceCaller.BatchDeleteAsync(ids);

    /// <summary>
    /// 添加酒店房间
    /// </summary>
    public Task AddRoomAsync(long id, AddHotelRoomDto dto)
        => _hotelServiceCaller.AddRoomAsync(id, dto);

    /// <summary>
    /// 更新酒店房间信息
    /// </summary>
    public Task UpdateRoomAsync(long id, long roomId, UpdateHotelRoomDto dto)
        => _hotelServiceCaller.UpdateRoomAsync(id, roomId, dto);

    /// <summary>
    /// 删除酒店房间
    /// </summary>
    public Task DeleteRoomAsync(long id, long roomId)
        => _hotelServiceCaller.DeleteRoomAsync(id, roomId);

    /// <summary>
    /// 批量删除酒店房间
    /// </summary>
    public Task BatchDeleteRoomAsync(long id, long[] roomIds)
        => _hotelServiceCaller.BatchDeleteRoomAsync(id, roomIds);
}
