using Core.Plugin.Web.MinimalAPIs.Validation.FluentValidation;
using Dida.Waylen.Onboarding.Demo.Service.Open.Applications.Hotels.Commands;
using Dida.Waylen.Onboarding.Demo.Service.Open.Applications.Hotels.Queries;
using Framework.Service.MinimalAPIs.Attributes;

namespace Dida.Waylen.Onboarding.Demo.Service.Open.Services;

/// <summary>
/// 酒店服务类，提供酒店及其房间的增删改查功能
/// </summary>
[AutoValidation]
public class HotelServicecs : ServiceBase
{
    /// <summary>
    /// 获取酒店分页列表
    /// </summary>
    /// <param name="localEventBus">本地事件总线</param>
    /// <param name="dto">分页查询参数</param>
    /// <returns>酒店分页数据</returns>
    public async Task<PagedResultDto<HotelDto>> GetPagedAsync([FromServices] ILocalEventBus localEventBus, [AsParameters] GetHotelPagedDto dto)
    {
        var query = new HotelPagedQuery(dto);
        await localEventBus.PublishAsync(query);

        return query.Result;
    }

    /// <summary>
    /// 获取酒店详细信息
    /// </summary>
    /// <param name="localEventBus">本地事件总线</param>
    /// <param name="id">酒店ID</param>
    /// <returns>酒店详细信息</returns>
    public async Task<HotelDetailDto> GetAsync([FromServices] ILocalEventBus localEventBus, long id)
    {
        var query = new HotelDetailQuery(id);
        await localEventBus.PublishAsync(query);

        return query.Result;
    }

    /// <summary>
    /// 添加新酒店
    /// </summary>
    /// <param name="localEventBus">本地事件总线</param>
    /// <param name="dto">创建酒店数据</param>
    /// <returns>操作结果</returns>
    public Task AddAsync([FromServices] ILocalEventBus localEventBus, CreateHotelDto dto)
    {
        var command = new CreateHotelCommand(dto);
        return localEventBus.PublishAsync(command);
    }

    /// <summary>
    /// 更新酒店信息
    /// </summary>
    /// <param name="localEventBus">本地事件总线</param>
    /// <param name="id">酒店ID</param>
    /// <param name="dto">更新酒店数据</param>
    /// <returns>操作结果</returns>
    public Task UpdateAsync([FromServices] ILocalEventBus localEventBus, long id, UpdateHotelDto dto)
    {
        var command = new UpdateHotelCommand(id, dto);
        return localEventBus.PublishAsync(command);
    }

    /// <summary>
    /// 删除酒店
    /// </summary>
    /// <param name="localEventBus">本地事件总线</param>
    /// <param name="Id">酒店ID</param>
    /// <returns>操作结果</returns>
    public Task DeleteAsync([FromServices] ILocalEventBus localEventBus, long Id)
    {
        var command = new DeleteHotelCommand(Id);
        return localEventBus.PublishAsync(command);
    }

    /// <summary>
    /// 批量删除酒店
    /// </summary>
    /// <param name="localEventBus">本地事件总线</param>
    /// <param name="ids">酒店ID数组</param>
    /// <returns>操作结果</returns>
    [RoutePattern(HttpMethod = "Delete", Pattern = "Batch")]
    public Task TaskBatchDeleteAsync([FromServices] ILocalEventBus localEventBus, [FromQuery] long[] ids)
    {
        var command = new DeleteHotelCommand(ids);
        return localEventBus.PublishAsync(command);
    }

    /// <summary>
    /// 添加酒店房间
    /// </summary>
    /// <param name="localEventBus">本地事件总线</param>
    /// <param name="id">酒店ID</param>
    /// <param name="dto">添加房间数据</param>
    /// <returns>操作结果</returns>
    [RoutePattern(Pattern = "{id}/Room")]
    public Task AddRoomAsync([FromServices] ILocalEventBus localEventBus, long id, AddHotelRoomDto dto)
    {
        var command = new AddHotelRoomCommand(id, dto);
        return localEventBus.PublishAsync(command);
    }

    /// <summary>
    /// 更新酒店房间信息
    /// </summary>
    /// <param name="localEventBus">本地事件总线</param>
    /// <param name="id">酒店ID</param>
    /// <param name="roomId">房间ID</param>
    /// <param name="dto">更新房间数据</param>
    /// <returns>操作结果</returns>
    [RoutePattern(Pattern = "{id}/Room/{roomId}")]
    public Task UpdateRoomAsync([FromServices] ILocalEventBus localEventBus, long id, long roomId, UpdateHotelRoomDto dto)
    {
        var command = new UpdateHotelRoomCommand(id, roomId, dto);
        return localEventBus.PublishAsync(command);
    }

    /// <summary>
    /// 删除酒店房间
    /// </summary>
    /// <param name="localEventBus">本地事件总线</param>
    /// <param name="id">酒店ID</param>
    /// <param name="roomId">房间ID</param>
    /// <returns>操作结果</returns>
    [RoutePattern(Pattern = "{id}/Room/{roomId}")]
    public Task DeleteRoomAsync([FromServices] ILocalEventBus localEventBus, long id, long roomId)
    {
        var command = new RemoveHotelRoomCommand(id, roomId);
        return localEventBus.PublishAsync(command);
    }

    /// <summary>
    /// 批量删除酒店房间
    /// </summary>
    /// <param name="localEventBus">本地事件总线</param>
    /// <param name="id">酒店ID</param>
    /// <param name="roomIds">房间ID数组</param>
    /// <returns>操作结果</returns>
    [RoutePattern(HttpMethod = "Delete", Pattern = "Room/Batch")]
    public Task BatchDeleteRoomAsync([FromServices] ILocalEventBus localEventBus, long id, [FromQuery] long[] roomIds)
    {
        var command = new RemoveHotelRoomCommand(id, roomIds);
        return localEventBus.PublishAsync(command);
    }
}
