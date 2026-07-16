
using CinemeBooking.Domain.Entities.Movies;
using CinemeBooking.Services.Interfaces.Movies;
using System.Text.Json;
using CinemeBooking.Domain.Enums;

namespace CinemaBooking.Services.Implementations;

// Service là trung gian lấy dữ liệu đẩy đến Implementations => Interface => Controller
public class MovieService : IMovieService
{
    // Thêm mới phim
    public async Task StoreMovieAsync(string[] movie)
    {
        // Gọi Model chuẩn bị lưu dữ liệu theo định dạng model
        Movie newMovie = new Movie
        {
            MovieName = movie[0] ?? "Chưa đặt tên",
            GenreUuid = movie[1],
            Language = string.IsNullOrWhiteSpace(movie[2]) ? LanguageEnum.Vietnamese : Enum.Parse<LanguageEnum>(movie[2]),
            ReleaseDate = movie[3] == "" ? DateTime.Now.ToString("dd/MM/yyyy") : movie[3],
            Duration = int.TryParse(movie[4], out int d) ? d : 120,
            AgeCustomer = movie[5] == "" ? 13 : int.Parse(movie[5]),
            Status = movie[6] == "" ? StatusEnum.Active : Enum.Parse<StatusEnum>(movie[6]) // Giả sử trạng thái mặc định là Active
        };


        // Đọc file json theo đường dẫn
        string filePath = "data/movies.json";
        // Tạo ra một vùng nhớ tạm thời để lưu dữ liệu
        List<object> movieList = new List<object>();


        if (File.Exists(filePath))
        {
            string currentJson = await File.ReadAllTextAsync(filePath);
            movieList = JsonSerializer.Deserialize<List<object>>(currentJson) ?? new List<object>();
        }

        // Danh sách phim + 1 phim mới chuẩn bị thêm vào
        movieList.Add(newMovie);

        // Tạo thiết lập cấu hình theo cấu trúc json
        var options = new JsonSerializerOptions { WriteIndented = true };
        // Chuyển đổi danh sách "movieList" thành dạng options (json đã đinh nghĩa phía trên)
        string updatedJson = JsonSerializer.Serialize(movieList, options);
        // Tiến hành thao tác thêm mới liệu vòa file
        await File.WriteAllTextAsync(filePath, updatedJson);
    }

    public async Task UpdateMovieAsync(Movie movie)
    {


        // Đọc file json theo đường dẫn
        string filePath = "data/movies.json";

        // 1. Kiểm tra file trước khi đọc
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File không tồn tại!");
            return;
        }

        if (string.IsNullOrEmpty(movie.MovieUuid)) return;

        if (movie.MovieUuid == "") return;

        try
        {
            // 2. Đọc toàn bộ danh sách hiện tại từ file
            string jsonString = await File.ReadAllTextAsync(filePath);
            var movies = JsonSerializer.Deserialize<List<Movie>>(jsonString) ?? new List<Movie>();

            // 3. Tìm vị trí (Index) của bộ phim cũ trong danh sách
            int index = movies.FindIndex(m => m.MovieUuid == movie.MovieUuid);

            // Nếu nó không tìm thấy index sẽ mặc định trả về -1
            if (index == -1)
            {
                Console.WriteLine("Không tìm thấy bộ phim cần cập nhật!");
                return;
            }

            // 4. Ghi đè bộ phim mới vào đúng vị trí của bộ phim cũ
            movies[index] = movie;

            // 5. Lưu lại toàn bộ danh sách đã sửa vào file JSON
            var options = new JsonSerializerOptions { WriteIndented = true };
            string updatedJson = JsonSerializer.Serialize(movies, options);

            await File.WriteAllTextAsync(filePath, updatedJson);
            Console.WriteLine("Cập nhật phim thành công! Thao tác bất kì đề tiếp tục");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi: {ex.Message}");
        }
    }

    // Gọi lấy danh sách phim dữ liệu từ file json + mapping Genre
    public async Task<List<Movie>> ListMovieAsync()
    {
        // Lấy danh sách phim từ chuỗi json
        // 1. Kiểm tra file và đọc danh sách phim
        string moviePath = "data/movies.json";


        if (!File.Exists(moviePath)) return new List<Movie>();

        string currentJson = await File.ReadAllTextAsync(moviePath);
        var movieDataJson = JsonSerializer.Deserialize<List<Movie>>(currentJson) ?? new List<Movie>();

        // Nếu không có phim nào, trả về danh sách rỗng luôn
        if (movieDataJson.Count == 0) return movieDataJson;

        // 2. Kiểm tra file và đọc danh sách thể loại
        string genrePath = "data/genres.json";
        if (File.Exists(genrePath))
        {
            string genreCurrentJson = await File.ReadAllTextAsync(genrePath);
            var genresDataJson = JsonSerializer.Deserialize<List<JsonElement>>(genreCurrentJson) ?? new List<JsonElement>();

            if (genresDataJson.Count > 0)
            {
                // Khởi tạo từ điển tra cứu thể loại
                var genreList = new Dictionary<string, Genre>();

                foreach (var item in genresDataJson)
                {
                    try
                    {
                        var g = JsonSerializer.Deserialize<Genre>(item.GetRawText());
                        if (g != null)
                        {
                            // Ưu tiên g.GenreUuid, nếu null thì lấy từ thuộc tính Json thô
                            string genreUuid = g.GenreUuid ?? item.GetProperty("GenreUuid").GetString() ?? string.Empty;

                            if (!string.IsNullOrEmpty(genreUuid))
                            {
                                genreList[genreUuid] = g;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Lỗi: " + e.Message);
                        Console.WriteLine("Vị trí lỗi: " + e.StackTrace);
                    }
                }
                return movieDataJson;
            }
        }

        // Trường hợp không có file thể loại hoặc file thể loại rỗng: 
        return movieDataJson;
    }

    // Action xử lý gọi model xóa phim (Xóa mềm)
    public async Task<int> DestroyMovieAsync(string movieUuid)
    {
        if (string.IsNullOrWhiteSpace(movieUuid)) return 0;

        try
        {
            string filePath = "data/movies.json";
            if (!File.Exists(filePath)) return 0;

            string jsonString = await File.ReadAllTextAsync(filePath);
            var movies = JsonSerializer.Deserialize<List<Movie>>(jsonString) ?? new List<Movie>();

            // lấy lên vị trí phần tử trong file để cập nhật vị trí
            int index = movies.FindIndex(m => m.MovieUuid == movieUuid);
            if (index == -1) return 0;

            movies[index].Status = StatusEnum.Deleted;

            var options = new JsonSerializerOptions { WriteIndented = true };
            await File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(movies, options));

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"XÓA {movies[index].MovieName} THÀNH CÔNG!");
            Console.WriteLine($"THAO TÁC BẤT KÌ ĐỂ QUAY LẠI MÀN HÌNH DANH SÁCH PHIM!");
            Console.ResetColor();
            Console.ReadKey();
            return 1;
        }
        catch (Exception)
        {
            // Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Thao tác xóa thất bại!");
            Console.ResetColor();
            return 0;
        }
    }
}