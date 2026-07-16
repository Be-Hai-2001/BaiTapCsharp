
using CinemeBooking.Domain.Entities.Cinemas;

namespace CinemaBooking.Services.Interfaces.Cinema;

public interface IRoomService
{
    Task<bool> StoreRoomAsync(string[] room);
    Task<bool> UpdateRoomAsync(Room room);
    Task<bool> DestroyRoomAsync(string roomUuid);
    Task<List<Room>> ListRoomAsync();
}