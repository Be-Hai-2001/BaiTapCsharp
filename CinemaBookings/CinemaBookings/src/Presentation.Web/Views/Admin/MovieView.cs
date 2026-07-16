
using CinemaBooking.Presentation.Controllers;
using CinemaBooking.Presentation.Views.Admin.Layout;
using CinemaBooking.Presentation.Views.Layout;
using CinemeBooking.Domain.Entities.Movies;

namespace CinemaBooking.Presentation.Views.Admin;

/// <summary>
/// Màn hình danh sách phim
/// </summary>
public class MovieView
{
    private enum EnumOption
    {
        UPDATE,
        DESTROY
    }
    public static async Task List(List<Movie> movies, MovieController movieController, IServiceProvider provider)
    {
        // string formatColumn = "| {0,-3} | {1,-30} | {2,-13} | {3,-10} | {4,-11} |";
        string formatColumn = "| {0,-3} | {1,-25} | {2,-9} | {3,-10} | {4,-10} | {5,-7} |";
        string lineDivider = "+-----+---------------------------+-----------+------------+------------+---------+";
        int tototalTableWidth = 83;
        int countMovie = movies.Count;


        bool isRunning = true;
        while (isRunning)
        {
            // ================== VẼ TOÀN BỘ GIAO DIỆN ================== //
            Console.Clear();
            UILayout.Header("DANH SÁCH PHIM", tototalTableWidth);

            Console.WriteLine(lineDivider);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(formatColumn, "STT", "Tên Phim", "Thể Loại", "Thời Lượng", "Ngôn Ngữ", "TT");
            Console.ResetColor();
            Console.WriteLine(lineDivider);

            if (countMovie > 0)
            {
                for (int index = 0; index < countMovie; index++)
                {
                    string movieName = movies[index].MovieName ?? string.Empty;
                    string genreName = movies[index].Genre?.GenreName ?? string.Empty;

                    // Thêm ... cho cái tên phim nếu quá dài
                    if (movieName.Length > 25)
                        movieName = movieName.Substring(0, 22) + "...";

                    // Thêm ... cho thể loại phim nếu quá dài
                    if (genreName.Length > 9)
                        genreName = genreName.Substring(0, 7) + "...";

                    Console.WriteLine(formatColumn, index, movieName, genreName, movies[index].Duration + " phút", movies[index].Language ?? null, movies[index].Status);

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(lineDivider);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            else
                Console.WriteLine("Không có phim...");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-----------------------------------------------------------------------------------");
            Console.ResetColor();

            // Lưu lại vị trí con trỏ để nhập STT
            int cursorLeft = Console.CursorLeft;
            int cursorTop = Console.CursorTop;

            // Đẩy Footer xuống dưới để giao diện cân đối
            Console.WriteLine("\n\n");
            UILayout.Footer("[F8]: Thêm mới | [F2]: Xóa | [F4]: Chỉnh sửa | [ESC]: Thoát ra ngoài", tototalTableWidth);

            // ================== VÒNG LẶP ĐỌC PHÍM TỪNG KÝ TỰ ( THAY THẾ READLINE ) ================== //
            while (true)
            {
                // Ở ĐÂY TA SẼ LẤY VỊ TRÍ HIỂN THỊ Ở TRONG DANH SÁCH => SAU ĐÓ SẼ LẤY RA CÁI PHIM => TT LẤY CÁI MÃ PHIM ĐI XỬ LÝ THAO TÁC...
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                // 1. LUÔN LUÔN BẮT ĐƯỢC F8 Ở ĐÂY
                if (keyInfo.Key == ConsoleKey.F8)
                {
                    Console.Clear();
                    await movieController.CreateMovie();
                }

                // 2. LUÔN LUÔN BẮT ĐƯỢC ESC Ở ĐÂY
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    await ShowMovieOrGenre.Show(provider);
                }

                // 3. BẮT THAO TÁC NGƯỜI DÙNG BẤM F4 => CHỈNH SỬA PHIM
                else if (keyInfo.Key == ConsoleKey.F4)
                    await OperationMovie(EnumOption.UPDATE, movies, countMovie, cursorLeft, cursorTop, "> Nhập STT phim bạn muốn chỉnh sửa", movieController, provider);

                //  3. BẮT THAO TÁC NGƯỜI DÙNG BẤM F2 => XÓA PHIM
                else if (keyInfo.Key == ConsoleKey.F2)
                    await OperationMovie(EnumOption.DESTROY, movies, countMovie, cursorLeft, cursorTop, "> Nhập STT phim bạn muốn xóa", movieController, provider);
            }
        }
    }

    private static async Task OperationMovie(EnumOption action, List<Movie> movies, int countMovie, int cursorLeft, int cursorTop, string title, MovieController movieController, IServiceProvider provider)
    {
        string inputStt = "";
        int? chosenStt = null;

        while (true)
        {
            // Xóa hết text cũ rồi đưa con trỏ trở lại đầu dòng
            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.Write(new string(' ', Console.WindowWidth - cursorLeft));
            Console.SetCursorPosition(cursorLeft, cursorTop);

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"{title}: {inputStt}");
            Console.ResetColor();
            ConsoleKeyInfo subKey = Console.ReadKey(true);

            // XỬ LÝ NGƯỜI DÙNG NHẤN ENTER
            if (subKey.Key == ConsoleKey.Enter)
            {
                // Chuyển string thành int đồng thời kiểm tra có nằm trong danh sách hay không
                if (int.TryParse(inputStt, out int parsedId) && parsedId >= 0 && parsedId < countMovie)
                {
                    chosenStt = parsedId;
                    Console.Clear();

                    if (action == EnumOption.UPDATE)
                        await movieController.EditMovie(movies[chosenStt.Value]);

                    if (action == EnumOption.DESTROY)
                    {
                        while (true)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write($"Xác nhận xóa phim: {movies[chosenStt.Value].MovieName} này? (Y/N): ");
                            Console.ResetColor();
                            var confirmKey = Console.ReadKey(true);

                            if (confirmKey.Key == ConsoleKey.Y)
                            {
                                await movieController.DestroyMovie(movies[chosenStt.Value].MovieUuid);
                                break;
                            }
                            else if (confirmKey.Key == ConsoleKey.N)
                            {
                                await movieController.List();
                                break;
                            }
                        }
                    }
                }
                else
                {
                    // Báo lỗi sai STT trực tiếp tại dòng nhập
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(" -> Lỗi: STT không hợp lệ!");
                    Console.ResetColor();
                    Thread.Sleep(1000);

                    // Xóa dòng lỗi để người dùng nhập lại
                    Console.SetCursorPosition(cursorLeft, cursorTop);
                    Console.Write(new string(' ', Console.WindowWidth - cursorLeft));
                    Console.SetCursorPosition(cursorLeft, cursorTop);
                    inputStt = "";
                }
            }
            // NẾU BẤM BACKSPACE THÌ CHO PHÉP XÓA KÝ TỰ VỪA GÕ
            else if (subKey.Key == ConsoleKey.Backspace && inputStt.Length > 0)
            {
                inputStt = inputStt.Substring(0, inputStt.Length - 1);
                // DebugExtensions.dd(inputStt);
                Console.Write("\b \b"); // Lùi con trỏ và xóa ký tự trên màn hình
            }
            // NẾU NHẬP SỐ THÌ HIỂN THỊ LÊN MÀN HÌNH
            else if (char.IsDigit(subKey.KeyChar))
            {
                inputStt += subKey.KeyChar;
                Console.Write(subKey.KeyChar);
            }
            // XỬ LÝ NGƯỜI DÙNG NHẤN LẠI F8
            else if (subKey.Key == ConsoleKey.F8)
            {
                Console.Clear();
                await movieController.CreateMovie();
            }

            // XỬ LÝ NGƯỜI DÙNG NHẤN ESC
            else if (subKey.Key == ConsoleKey.Escape)
            {
                Console.Clear();
                await ShowMovieOrGenre.Show(provider);
            }

            // NẾU NGƯỜI DÙNG NHẬP LAI F4
            else if (subKey.Key == ConsoleKey.F4)
            {
                await OperationMovie(EnumOption.UPDATE, movies, countMovie, cursorLeft, cursorTop, "> Nhập STT phim bạn muốn chỉnh sửa", movieController, provider);
            }
            // NẾU NGƯỜI DÙNG NHẬP LẠI F2
            else if (subKey.Key == ConsoleKey.F2)
            {
                await OperationMovie(EnumOption.DESTROY, movies, countMovie, cursorLeft, cursorTop, "> Nhập STT phim bạn muốn xóa", movieController, provider);
            }
        }
    }
}
