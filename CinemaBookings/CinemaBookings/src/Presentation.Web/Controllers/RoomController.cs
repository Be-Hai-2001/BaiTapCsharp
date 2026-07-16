
using CinemaBooking.Services.Implementations;
using CinemaBooking.Services.Interfaces.Cinema;
using CinemeBooking.Domain.Entities.Cinemas;

namespace CinemaBooking.Presentation.Controllers;

public class RoomController
{
    private IRoomService _roomService;
    private IServiceProvider _provider;

    public RoomController(IRoomService roomService, IServiceProvider provider)
    {
        _roomService = roomService;
        _provider = provider;
    }

    // Hiển thị danh sách tất cả các phòng chiếu có tại rạp
    public async Task List()
    {
        // Danh sách phim lấy từ file Json || DB
        List<Room> rooms = await _roomService.ListRoomAsync();

        // Màn hình danh sách phòng
        RoomView.List(rooms, this, _provider);
    }
}