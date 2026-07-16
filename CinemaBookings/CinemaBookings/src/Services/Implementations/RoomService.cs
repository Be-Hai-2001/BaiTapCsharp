
using CinemaBooking.Services.Interfaces.Cinema;
using CinemeBooking.Domain.Entities.Cinemas;
using CinemeBooking.Domain.Enums;
using System.Text.Json;

namespace CinemaBooking.Services.Implementations;

public class RoomService : IRoomService
{
    public async Task<bool> StoreRoomAsync(string[] room)
    {
        return true;
    }

    public async Task<bool> UpdateRoomAsync(Room room)
    {
        // return Task.FromResult(true);
        return true;
    }

    public async Task<bool> DestroyRoomAsync(string roomUuid)
    {
        return true;
    }

    public async Task<List<Room>> ListRoomAsync()
    {
        // Gọi đường dẫn lấy file
        string filePath = "data/rooms.json";
        // Tạo ra một vùng nhớ tạm thời để lưu dữ liệu
        List<Room> roomList = new List<Room>();


        if (File.Exists(filePath))
        {
            string currentJson = await File.ReadAllTextAsync(filePath);
            roomList = JsonSerializer.Deserialize<List<Room>>(currentJson) ?? new List<Room>();

            // Lọc chỉ các phòng có trạng thái Active
            roomList = roomList.Where(r => r.Status == StatusEnum.Active).ToList();
        }

        return roomList;
    }
}