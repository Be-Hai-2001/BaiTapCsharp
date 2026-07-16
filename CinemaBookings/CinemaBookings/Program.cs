// using CinemeBooking.Views;

// class Program
// {
//     static void Main(string[] args)
//     {
//         // 1. Khởi tạo các tầng repository / service (Ví dụ)
//         // IMovieService movieService = new MovieService();

//         // 2. Gọi hiển thị Menu
//         // MainMenuView.Show();
//     }
// }

using System.Text;
using Microsoft.Extensions.DependencyInjection;
using CinemaBooking.Presentation.Controllers;
using CinemaBooking.Services.Implementations;
using CinemeBooking.Views;
using CinemeBooking.Services.Interfaces.Movies;
using CinemaBooking.Services.Interfaces.Cinema;

class Program
{
    static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        // MainMenuView.Show();

        // Thiết lập DI container và đăng ký mapping interface -> implementation
        // Tạo ra container để đăng ký các service trước khi build thành provider
        var services = new ServiceCollection();

        // Đăng ký pinding provider cho movie
        services.AddTransient<IMovieService, MovieService>();
        services.AddTransient<MovieController>();

        // Đăng ký provider cho room
        services.AddTransient<IRoomService, RoomService>();
        services.AddTransient<RoomController>();

        // Đăng ký để sau này lấy ra action trong controller
        var provider = services.BuildServiceProvider();

        // Resolve MovieController từ container (container sẽ inject MovieService vào)
        var movieController = provider.GetRequiredService<MovieController>();

        // Hệ thống giao diện chính (truyền provider để resolve controller khi cần)
        await MainMenuView.Show(provider);

        // Danh sách phim
        // await movieController.List();
        // Thêm mới phim
        // await movieController.CreateMovie();
    }

}