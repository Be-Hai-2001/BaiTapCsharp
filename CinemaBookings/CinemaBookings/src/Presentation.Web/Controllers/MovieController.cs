
using CinemaBooking.Presentation.Views.Admin;
using CinemaBookings.Views.Admin.Layout;
using CinemeBooking.Services.Interfaces.Movies;
using CinemeBooking.Domain.Entities.Movies;

namespace CinemaBooking.Presentation.Controllers;

// Controller ở đây chỉ có chức năng điều hướng
public class MovieController
{
    private IMovieService _movieService;
    private IServiceProvider _provider;

    // Phương thức khởi tạo khi binding
    public MovieController(IMovieService movieService, IServiceProvider provider)
    {
        _movieService = movieService;
        _provider = provider;
    }

    public async Task CreateMovie()
    {

        string[] fields = {
        "Tên phim: ", "Thể loại: ", "Ngôn ngữ: ", "Ngày phát hành: ",
        "Thời lượng (phút): ", "Độ tuổi tối thiểu: ", "Trạng thái: "
        };

        // Theo logic inputs là một state dữ liệu tạm thời sẽ thay đổi và cơ chế nó là truyền tham chiếu
        string[] inputs = { "", "", "", "", "", "", "" };

        // 1. Gọi giao diện hiển thị form // Nghiệp vụ Request -> Controler -> View
        UpsertMovielLayout movieLayout = new("THÊM MỚI PHIM", fields, inputs, "Thêm Mới", this);
        await movieLayout.Create();

        // 3. Tiến hành xử lý sau khi người dùng nhập xong
        Console.Clear();
        Console.WriteLine("Đang xử lý dữ liệu và lưu vào hệ thống...");

        try
        {
            // Gọi Interface (Interface goi: Implementations) xử lý logic lõi
            await _movieService.StoreMovieAsync(inputs);
            // Nếu thành công thì hiển thị thông báo màu xanh ở tầng giao diện
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n🎉 THÊM MỚI PHIM THÀNH CÔNG!");
            Console.ResetColor();

        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Có lỗi xảy ra khi lưu: {ex.Message}");
            Console.WriteLine($"Vị trí xãy ra lỗi: {ex.StackTrace}");
            Console.ResetColor();
        }

        Console.WriteLine("Bấm phím bất kỳ để quay lại.");
        Console.ReadKey();

        // Điều hướng khi thêm mới thành công hiển thị danh sách phim
        await this.List();

    }

    // Action hiển Thị trang chỉnh sửa
    public async Task EditMovie(Movie movie)
    {
        string[] fields = {
            "Tên phim: ", "Thể loại: ", "Ngôn ngữ: ", "Ngày phát hành: ",
            "Thời lượng (phút): ", "Độ tuổi tối thiểu: "
        };

        // Theo logic inputs là một state dữ liệu tạm thời sẽ thay đổi và cơ chế nó là truyền tham chiếu
        string[] inputs = { "", "", "", "", "", "", "" };

        // 1. Gọi giao diện hiển thị form // Nghiệp vụ Request -> Controler -> View
        UpsertMovielLayout movieLayout = new("CẬP NHẬT THÔNG TIN PHIM", fields, inputs, "Thêm Mới", this);
        await movieLayout.Edit(movie);
    }

    /*
        Controller nhận vào "movie" và gọi Interface xử lý cập nhật
        Action cập nhật sản phẩm khi người dùng Chọn thao tác cập nhật
        Prop: Movie movie
        return: Trả về trang cập nhật với giá trị mới 
    */
    public async Task UpdateMovie(Movie movie)
    {
        // 1. Tiến hành cập nhật dữ liệu mới
        await _movieService.UpdateMovieAsync(movie);

        // 2. Điều hướng sang trang chỉnh sửa cho admin xem nội dung vừa mới cập nhật
        await this.EditMovie(movie);
    }

    public async Task DestroyMovie(string movieUuid)
    {
        // Tiến hành xóa mềm
        int statusDestroy = await _movieService.DestroyMovieAsync(movieUuid);

        // Tiến hành điều hướng khi đã xóa thành công
        if (statusDestroy == 1)
        {
            // Lưu cập nhật sản phẩm thành công
            await this.List();
        }
        else
        {
            // Lưu cập nhật sản phẩm thất bại
            // Console.WriteLine("Thất bại");
            // Console.ReadKey();
        }
    }

    /*
        Hiển thị danh sách phim với trạng thái Active    
    */
    public async Task List()
    {
        // Danh sách phim
        List<Movie> movies = await _movieService.ListMovieAsync();

        // Duyệt danh sách. không lấy fim có trạng thái delete
        // movies.RemoveAll(movie => movie.Status.HasValue && (int)movie.Status.Value == -1);

        // Truyền danh sách phim vào view danh sách phim (inject controller from DI)
        await MovieView.List(movies, this, _provider);
    }
}


