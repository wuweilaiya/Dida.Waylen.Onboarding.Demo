namespace Dida.Waylen.Onboarding.Demo.Service.Open.Tests.Data.Entities;

public class HotelTests
{
    private readonly Hotel _hotel;

    public HotelTests()
    {
        _hotel = new Hotel
        {
            Id = GetId(),
            Name = "Test",
            Address = new Address
            {
                Latitude = 45,
                Longitude = 45,
                Detail = "test"
            },
            Contact = new Contact
            {
                PhoneNumber = "15168440232"
            }
        };
    }

    [Fact]
    public void AddRooms_ShouldAddRoomsToHotel()
    {
        // Arrange
        var room1 = new Room { Id = GetId(), Number = "101" };
        var room2 = new Room { Id = GetId(), Number = "102" };
        var room3 = new Room { Id = GetId(), Number = "101" };

        // Act
        _hotel.AddRooms(room1, room2);

        // Assert
        Assert.Equal(2, _hotel.Rooms.Count);
        Assert.Contains(room1, _hotel.Rooms);
        Assert.Contains(room2, _hotel.Rooms);
        Assert.Throws<FriendlyException>(() => _hotel.AddRooms(room3));
    }

    [Fact]
    public void RemoveRooms_ShouldRemoveRoomsFromHotel()
    {
        // Arrange
        var room1 = new Room { Id = GetId(), Number = "101" };
        var room2 = new Room { Id = GetId(), Number = "102" };
        _hotel.AddRooms(room1, room2);

        // Act
        _hotel.RemoveRooms([room1.Id]);

        // Assert
        Assert.Contains(room2, _hotel.Rooms);
        Assert.True(!_hotel.Rooms.Contains(room1));
    }

    [Fact]
    public void UpdateRoom_ShouldUpdateRoomInfo()
    {
        // Arrange
        var room = new Room { Id = GetId(), Number = "101" };
        _hotel.AddRooms(room);
        var updateDto = new UpdateHotelRoomDto
        {
            Number = "102",
            Type = RoomTypeEnum.Deluxe
        };

        // Act
        _hotel.UpdateRoom(room.Id, updateDto);

        // Assert
        var updatedRoom = _hotel.Rooms.First();
        Assert.Equal("102", updatedRoom.Number);
        Assert.Equal(RoomTypeEnum.Deluxe, updatedRoom.Type);
    }

    static long GetId()
    {
        return new Random().NextInt64(long.MinValue, long.MaxValue);
    }
}
