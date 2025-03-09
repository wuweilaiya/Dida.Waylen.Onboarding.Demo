using Dida.Waylen.Onboarding.Demo.Service.Open.Applications.Hotels.Commands;
using Dida.Waylen.Onboarding.Demo.Service.Open.Applications.Hotels.Events;
using Dida.Waylen.Onboarding.Demo.Service.Open.Infrastructure.Events;
using Dida.Waylen.Onboarding.Demo.Shared.Enums;

namespace Dida.Waylen.Onboarding.Demo.Service.Open.Applications.Hotels;

public class HotelCommandHandler(DemoDbContext _didaDbContext, ILocalEventBus _localEventBus, IUniversalIDGenerator _idGenerator)
{
    [LocalEventHandler]
    public async Task CreateAsync(CreateHotelCommand command)
    {
        var hotel = command.Dto.Adapt<Hotel>();
        hotel.Id = _idGenerator.NewLongID();
        await _didaDbContext.Hotels.AddAsync(hotel);
        await PublishHotelChangedEventAsync(hotel, EntityChangedTypeEnum.Created);
        //await _didaDbContext.SaveChangesAsync();
    }

    [LocalEventHandler]
    public async Task UpdateAsync(UpdateHotelCommand command)
    {
        var hotel = await _didaDbContext.Hotels.FirstAsync(e => e.Id == command.Id);
        hotel = command.Dto.Adapt(hotel);
        _didaDbContext.Update(hotel);
        await PublishHotelChangedEventAsync(hotel, EntityChangedTypeEnum.Updated);
        //await _didaDbContext.SaveChangesAsync();
    }

    [LocalEventHandler]
    public async Task DeleteAsync(DeleteHotelCommand command)
    {
        var hotels = await _didaDbContext.Hotels.Include(e => e.Rooms)
                                                .Where(e => command.Ids.Contains(e.Id))
                                                .ToListAsync();
        _didaDbContext.RemoveRange(hotels);
        await _localEventBus.PublishAsync(new HotelBatchDeletedEvent(hotels));
        //await _didaDbContext.SaveChangesAsync();
    }

    [LocalEventHandler]
    public async Task AddRoomsAsync(AddHotelRoomCommand command)
    {
        var hotel = await _didaDbContext.Hotels.Include(e => e.Rooms).FirstAsync(e => e.Id == command.HotelId);
        var rooms = command.Dto.Adapt<Room[]>();
        foreach (var item in rooms)
        {
            item.Id = _idGenerator.NewLongID();
        }
        hotel.AddRooms(rooms);
        _didaDbContext.Update(hotel);
        await PublishHotelChangedEventAsync(hotel, EntityChangedTypeEnum.Updated);
        //await _didaDbContext.SaveChangesAsync();
    }

    [LocalEventHandler]
    public async Task UpdateRoomAsync(UpdateHotelRoomCommand command)
    {
        var hotel = await _didaDbContext.Hotels.FirstAsync(e => e.Id == command.HotelId);
        hotel.UpdateRoom(command.RoomId, command.Dto);
        _didaDbContext.Update(hotel);
        await PublishHotelChangedEventAsync(hotel, EntityChangedTypeEnum.Updated);
        //await _didaDbContext.SaveChangesAsync();
    }

    [LocalEventHandler]
    public async Task RemoveRoomsAsync(RemoveHotelRoomCommand command)
    {
        var hotel = await _didaDbContext.Hotels.Include(e => e.Rooms).FirstAsync(e => e.Id == command.HotelId);
        hotel.RemoveRooms(command.RoomIds);
        _didaDbContext.Update(hotel);
        await PublishHotelChangedEventAsync(hotel, EntityChangedTypeEnum.Updated);
        //await _didaDbContext.SaveChangesAsync();
    }

    Task PublishHotelChangedEventAsync(Hotel hotel, EntityChangedTypeEnum type)
    {
        return _localEventBus.PublishAsync(new EntityChangedEvent<Hotel>(hotel, type));
    }
}
