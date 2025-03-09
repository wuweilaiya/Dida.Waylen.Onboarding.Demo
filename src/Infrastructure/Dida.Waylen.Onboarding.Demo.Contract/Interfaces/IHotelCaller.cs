using Core.DataModel.Dtos;
using Dida.Waylen.Onboarding.Demo.Shared.Model.Dtos.Hotels;
using Dida.Waylen.Onboarding.Demo.Shared.Model.Dtos.Rooms;

namespace Dida.Waylen.Onboarding.Demo.Contract.Interfaces;

public interface IHotelCaller
{
    /// <summary>
    /// 获取酒店分页列表
    /// </summary>
    /// <param name="dto">分页查询参数</param>
    /// <returns>酒店分页数据</returns>
    Task<PagedResultDto<HotelDto>> GetPagedAsync(GetHotelPagedDto dto);

    /// <summary>
    /// 获取酒店详细信息
    /// </summary>
    /// <param name="id">酒店ID</param>
    /// <returns>酒店详细信息</returns>
    Task<HotelDetailDto> GetAsync(long id);

    /// <summary>
    /// 添加新酒店
    /// </summary>
    /// <param name="dto">创建酒店数据</param>
    /// <returns>操作结果</returns>
    Task AddAsync(CreateHotelDto dto);

    /// <summary>
    /// 更新酒店信息
    /// </summary>
    /// <param name="id">酒店ID</param>
    /// <param name="dto">更新酒店数据</param>
    /// <returns>操作结果</returns>
    Task UpdateAsync(long id, UpdateHotelDto dto);

    /// <summary>
    /// 删除酒店
    /// </summary>
    /// <param name="localEventBus">本地事件总线</param>
    /// <param name="Id">酒店ID</param>
    /// <returns>操作结果</returns>
    Task DeleteAsync(long Id);

    /// <summary>
    /// 批量删除酒店
    /// </summary>
    /// <param name="localEventBus">本地事件总线</param>
    /// <param name="ids">酒店ID数组</param>
    /// <returns>操作结果</returns>
    Task TaskBatchDeleteAsync(long[] ids);

    /// <summary>
    /// 添加酒店房间
    /// </summary>
    /// <param name="localEventBus">本地事件总线</param>
    /// <param name="id">酒店ID</param>
    /// <param name="dto">添加房间数据</param>
    /// <returns>操作结果</returns>
    Task AddRoomAsync(long id, AddHotelRoomDto dto);

    /// <summary>
    /// 更新酒店房间信息
    /// </summary>
    /// <param name="id">酒店ID</param>
    /// <param name="roomId">房间ID</param>
    /// <param name="dto">更新房间数据</param>
    /// <returns>操作结果</returns>
    Task UpdateRoomAsync(long id, long roomId, UpdateHotelRoomDto dto);

    /// <summary>
    /// 删除酒店房间
    /// </summary>
    /// <param name="id">酒店ID</param>
    /// <param name="roomId">房间ID</param>
    /// <returns>操作结果</returns>
    Task DeleteRoomAsync(long id, long roomId);

    /// <summary>
    /// 批量删除酒店房间
    /// </summary>
    /// <param name="id">酒店ID</param>
    /// <param name="roomIds">房间ID数组</param>
    /// <returns>操作结果</returns>
    Task BatchDeleteRoomAsync(long[] roomIds);
}
