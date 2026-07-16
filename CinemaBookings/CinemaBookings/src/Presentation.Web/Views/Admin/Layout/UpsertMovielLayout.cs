using System.Text.Json;
using CinemeBooking.Domain.Entities.Movies;
using CinemeBooking.Domain.Enums;
using CinemaBooking.Presentation.Views.Layout;
using CinemaBooking.Presentation.Controllers;

namespace CinemaBookings.Views.Admin.Layout
{
    public class UpsertMovielLayout
    {
        public string Title { get; set; } // Dữ  liệu bình thường, có hay không vẫn được
        public string[] Fields { get; set; }

        public string? ButtonText { get; set; }
        // Lưu dữ liệu người dùng thao tác trên UI ( Xử lý người dùng chọn tên ta lưu mã)
        public string[] Inputs { get; set; }

        // Dữ liệu của danh sách hiển thị khi người dùng thao tác nhập liệu trên UI
        private string[] DataProp = { "", "", "", "", "", "", "" };

        private List<Genre> GenreList = new List<Genre>();
        private List<LanguageEnum> Languages = new List<LanguageEnum> { LanguageEnum.Vietnamese, LanguageEnum.English, LanguageEnum.Japanese, LanguageEnum.Chinese };
        private List<StatusEnum> Status = new List<StatusEnum> { StatusEnum.Active, StatusEnum.Inactive };

        private int _currentGenreIndex, _currentLanguageIndex, _currentStatusIndex = 0;

        // MovieController được inject từ bên ngoài (qua DI/resolver)
        private MovieController _movieController;

        // PHƯƠNG THỨC KHỞI TẠO CỦA CLASS
        public UpsertMovielLayout(string title, string[] fields, string[] inputs, string? buttonText, MovieController movieController)
        {
            Title = title;
            Fields = fields;
            Inputs = inputs;
            ButtonText = buttonText;

            _movieController = movieController;

            // Đọc file JSON 1 lần duy nhất lúc khởi tạo để tránh giảm hiệu năng / giật màn hình
            try
            {
                if (File.Exists("data/genres.json"))
                {
                    string jsonString = File.ReadAllText("data/genres.json");
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    GenreList = JsonSerializer.Deserialize<List<Genre>>(jsonString, options) ?? new List<Genre>();
                }
            }
            catch (Exception)
            {
                // Xử lý log lỗi nếu cần thiết
                GenreList = new List<Genre>();
            }
        }

        public async Task Create()
        {
            int tototalTableWidth = 83;
            // Đồng bộ giá trị mặc định ban đầu cho trường Thể loại (Index 1)
            if (GenreList.Count > 0 && DataProp.Length > 1 && Inputs.Length > 1)
            {
                DataProp[1] = GenreList[_currentGenreIndex].GenreName ?? "";
                Inputs[1] = GenreList[_currentGenreIndex].GenreUuid ?? "";
            }

            // Khởi tạo giá trị mặc định ListBox ngôn ngữ (Index 2)
            if (Languages.Count > 0 && DataProp.Length > 2 && Inputs.Length > 2)
            {
                DataProp[2] = Languages[_currentLanguageIndex].ToString();
                Inputs[2] = Languages[_currentLanguageIndex].ToString();
            }

            // Khởi tạo giá trị mặc định ListBox trạng thái (Index 6)
            if (Status.Count > 0 && DataProp.Length > 6 && Inputs.Length > 6)
            {
                DataProp[6] = Status[_currentStatusIndex].ToString();
                Inputs[6] = Status[_currentStatusIndex].ToString();
            }

            int currentField = 0;
            Console.Clear();
            Console.CursorVisible = false;

            try
            {
                if (OperatingSystem.IsWindows())
                {
                    if (Console.WindowWidth < 60) Console.WindowWidth = 60;
                    if (Console.WindowHeight < 20) Console.WindowHeight = 20;
                }
            }
            catch { }

            while (true)
            {
                // Đưa con trỏ về (0,0) thay vì Clear liên tục giúp giao diện mượt hơn rất nhiều
                Console.SetCursorPosition(0, 0);

                // Vẽ giao diện Header
                UILayout.Header(Title, tototalTableWidth);

                // VẼ GIAO DIỆN FORM CHỈNH SỬA -> BODY
                string format = "> {0,-20} {1}";
                for (int i = 0; i < Fields.Length; i++)
                {
                    bool isSelected = (i == currentField);

                    // Tạo nhãn (Label) - Bỏ dấu "> " ở đây vì ta đã đưa nó vào chuỗi format chung phía trên rồi
                    string label = Fields[i];

                    // Xác định giá trị hiển thị (Value)
                    string valueInput = "";
                    if (i == 1 || i == 2 || i == 6) // Listbox select
                    {
                        valueInput = isSelected ? $"< {DataProp[i]} >" : $"  {DataProp[i]}  ";
                    }
                    else // Ô text nhập bình thường
                    {
                        valueInput = Inputs[i];
                    }

                    // Đổi màu chữ nếu dòng này đang được chọn (Selected)
                    if (isSelected)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                    }

                    // IN RA CẢ 2 CỘT TRÊN CÙNG MỘT DÒNG ĐÚNG THEO FORMAT
                    // Dùng thêm PadRight để xóa ký tự thừa của lần render trước nếu chuỗi mới ngắn hơn
                    string fullLine = string.Format(format, label, valueInput);
                    Console.WriteLine(fullLine.PadRight(60));
                    Console.WriteLine("".PadRight(10));

                    // Reset lại màu chữ mặc định cho dòng sau
                    Console.ResetColor();
                }

                // VẼ GIAO DIỆN FOOTER
                UILayout.Footer("[F8]: Bấm để Thêm Mới | [ESC]: Thoát ra ngoài", tototalTableWidth);

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                // 1. Điều hướng lên / xuống
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    currentField = (currentField + 1) % Fields.Length;
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    currentField = (currentField - 1 + Fields.Length) % Fields.Length;
                }

                // 2. Thao tác Listbox qua lại (Mũi tên trái / phải)
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    if (currentField == 1 && GenreList.Count > 0)
                    {
                        _currentGenreIndex = (_currentGenreIndex + 1) % GenreList.Count;
                        DataProp[1] = GenreList[_currentGenreIndex].GenreName ?? "";
                        Inputs[1] = GenreList[_currentGenreIndex].GenreUuid ?? "";
                    }
                    else if (currentField == 2 && Languages.Count > 0)
                    {
                        _currentLanguageIndex = (_currentLanguageIndex + 1) % Languages.Count;
                        DataProp[2] = Languages[_currentLanguageIndex].ToString();
                        Inputs[2] = Languages[_currentLanguageIndex].ToString();
                    }
                    else if (currentField == 6 && Status.Count > 0)
                    {
                        _currentStatusIndex = (_currentStatusIndex + 1) % Status.Count;
                        DataProp[6] = Status[_currentStatusIndex].ToString();
                        Inputs[6] = Status[_currentStatusIndex].ToString();
                    }
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    if (currentField == 1 && GenreList.Count > 0)
                    {
                        _currentGenreIndex = (_currentGenreIndex - 1 + GenreList.Count) % GenreList.Count;
                        DataProp[1] = GenreList[_currentGenreIndex].GenreName ?? "";
                        Inputs[1] = GenreList[_currentGenreIndex].GenreUuid ?? "";
                    }
                    else if (currentField == 2 && Languages.Count > 0)
                    {
                        _currentLanguageIndex = (_currentLanguageIndex - 1 + Languages.Count) % Languages.Count;
                        DataProp[2] = Languages[_currentLanguageIndex].ToString();
                        Inputs[2] = Languages[_currentLanguageIndex].ToString();
                    }
                    else if (currentField == 6 && Status.Count > 0)
                    {
                        _currentStatusIndex = (_currentStatusIndex - 1 + Status.Count) % Status.Count;
                        DataProp[6] = Status[_currentStatusIndex].ToString();
                        Inputs[6] = Status[_currentStatusIndex].ToString();
                    }
                }

                // 3. Xử lý nút Xóa (Backspace) cho các trường nhập liệu thông thường
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (currentField != 1 && currentField != 2 && currentField != 6 && Inputs[currentField].Length > 0)
                    {
                        // XỬ LÝ RIÊNG CHO TRƯỜNG NGÀY PHÁT HÀNH (INDEX 3) KHI XÓA
                        // Nếu đang có dạng "dd/" (lên 3 ký tự) hoặc "dd/MM/" (lên 6 ký tự), xóa dấu '/' đồng thời xóa luôn số liền trước
                        if (currentField == 3 && (Inputs[3].Length == 3 || Inputs[3].Length == 6))
                        {
                            Inputs[3] = Inputs[3].Substring(0, Inputs[3].Length - 2);
                        }
                        else
                        {
                            Inputs[currentField] = Inputs[currentField].Substring(0, Inputs[currentField].Length - 1);
                        }
                    }
                }

                // 4. Thoát chương trình
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    await _movieController.List();
                }

                // 5. Xử lý lưu dữ liệu (F8)
                else if (keyInfo.Key == ConsoleKey.F8)
                {
                    if (Inputs[3] == "" || !Inputs[3].IsValidDateExact("dd/MM/yyyy"))
                    {
                        // In ra dòng lỗi đè cái footer
                        Console.SetCursorPosition(1, 17);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Lỗi: Ngày không hợp lệ hoặc sai định dạng (dd/MM/yyyy)!".PadRight(Console.WindowWidth - 1));
                        Console.ResetColor();

                        // Dừng màn hình cho người dung xem lỗi
                        Console.ReadKey(true);

                        // In đè một hàng khoảng trắng để xóa sạch dòng chữ đỏ cũ trả lại footer
                        Console.SetCursorPosition(1, 17);
                        Console.WriteLine("".PadRight(Console.WindowWidth - 1));

                        // Quay lại vòng lặp bắt người dùng tiếp tục nhập cho đến khi đúng
                        continue;
                    }

                    if (Inputs[0] == "")
                    {
                        // In ra dòng lỗi đè cái footer
                        Console.SetCursorPosition(1, 17);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Lỗi: Không được bỏ trống tên phim!".PadRight(Console.WindowWidth - 1));
                        Console.ResetColor();

                        // Dừng màn hình cho người dung xem lỗi
                        Console.ReadKey(true);

                        // In đè một hàng khoảng trắng để xóa sạch dòng chữ đỏ cũ trả lại footer
                        Console.SetCursorPosition(1, 17);
                        Console.WriteLine("".PadRight(Console.WindowWidth - 1));

                        // Quay lại vòng lặp bắt người dùng tiếp tục nhập cho đến khi đúng
                        continue;
                    }
                    return;
                }

                // 6. Nhập liệu ký tự từ bàn phím chữ/số
                else if ((keyInfo.KeyChar >= 32 && keyInfo.KeyChar <= 126) || keyInfo.KeyChar >= 192)
                {
                    // Giới hạn kí tự tên phim
                    if (currentField == 0)
                    {
                        if (Inputs[0].Length < 45)
                            Inputs[0] += keyInfo.KeyChar;
                    }
                    else if (currentField != 1 && currentField != 2 && currentField != 6)
                    {
                        // XỬ LÝ GÕ CHO TRƯỜNG NGÀY PHÁT HÀNH (INDEX 3)
                        if (currentField == 3)
                        {
                            if (char.IsDigit(keyInfo.KeyChar) && Inputs[3].Length < 10)
                            {
                                Inputs[3] += keyInfo.KeyChar;

                                // Tự động chèn thêm dấu '/' sau khi gõ xong ngày (2 số) hoặc tháng (5 ký tự dạng 'dd/MM')
                                if (Inputs[3].Length == 2 || Inputs[3].Length == 5)
                                {
                                    Inputs[3] += "/";
                                }
                            }
                        }
                        // Ràng buộc nhập thời lượng phim chiếu không quá xxxx
                        else if (currentField == 4)
                        {
                            if (char.IsDigit(keyInfo.KeyChar) && Inputs[4].Length < 4)
                                Inputs[4] += keyInfo.KeyChar;
                        }
                        // Độ tuổi tối thiểu không hơn xx
                        else if (currentField == 5)
                        {
                            if (char.IsDigit(keyInfo.KeyChar) && Inputs[5].Length < 2)
                                Inputs[5] += keyInfo.KeyChar;
                        }
                        else
                        {
                            // Nhập văn bản thông thường
                            if (Inputs[currentField].Length < 25)
                            {
                                Inputs[currentField] += keyInfo.KeyChar;
                            }
                        }
                    }
                }
            }
        }

        public async Task Edit(Movie movie)
        {
            // Độ dài
            int tototalTableWidth = 83;
            var countGenreList = GenreList.Count;
            var countLanguages = Languages.Count;

            // Đồng bộ giá trị mặc định ban đầu cho Các trường dữ liệu sẵn có
            DataProp[0] = movie.MovieName ?? "";
            Inputs[0] = movie.MovieName ?? "";

            DataProp[3] = movie.ReleaseDate ?? "";
            Inputs[3] = movie.ReleaseDate ?? "";

            DataProp[4] = movie.Duration.ToString();
            Inputs[4] = movie.Duration.ToString();

            DataProp[5] = movie.AgeCustomer.ToString();
            Inputs[5] = movie.AgeCustomer.ToString();

            if (movie.Genre != null && !string.IsNullOrEmpty(movie.Genre.GenreName))
            {
                DataProp[1] = movie.Genre.GenreName ?? "";
                Inputs[1] = movie.Genre.GenreUuid ?? "";
            }
            else if (countGenreList > 0 && DataProp.Length > 1 && Inputs.Length > 1)
            {
                DataProp[1] = GenreList[_currentGenreIndex].GenreName ?? "";
                Inputs[1] = GenreList[_currentGenreIndex].GenreUuid ?? "";
            }

            // Khởi tạo giá trị mặc định ListBox ngôn ngữ (Index 2)
            if (movie.Language != null)
            {
                DataProp[2] = ((LanguageEnum)Enum.ToObject(typeof(LanguageEnum), movie.Language)).ToString();
                Inputs[2] = ((LanguageEnum)Enum.ToObject(typeof(LanguageEnum), movie.Language)).ToString();
            }
            else if (countLanguages > 0 && DataProp.Length > 2 && Inputs.Length > 2)
            {
                DataProp[2] = Languages[_currentLanguageIndex].ToString();
                Inputs[2] = Languages[_currentLanguageIndex].ToString();
            }

            int currentField = 0;
            Console.Clear();
            Console.CursorVisible = false;

            try
            {
                if (OperatingSystem.IsWindows())
                {
                    if (Console.WindowWidth < 60) Console.WindowWidth = 60;
                    if (Console.WindowHeight < 20) Console.WindowHeight = 20;
                }
            }
            catch { }

            while (true)
            {
                int RELEASEDATELENGHT = Inputs[3].Length;
                // Đưa con trỏ về (0,0) thay vì Clear liên tục giúp giao diện mượt hơn rất nhiều
                Console.SetCursorPosition(0, 0);
                // VẼ GIAO DIỆN HEADER
                UILayout.Header(Title, tototalTableWidth);

                // VẼ GIAO DIỆN FORM CHỈNH SỬA -> BODY
                string format = "> {0,-20} {1}";
                for (int i = 0; i < Fields.Length; i++)
                {
                    bool isSelected = (i == currentField);

                    // Tạo nhãn (Label) - Bỏ dấu "> " ở đây vì ta đã đưa nó vào chuỗi format chung phía trên rồi
                    string label = Fields[i];

                    // Xác định giá trị hiển thị (Value)
                    string valueInput = "";
                    if (i == 1 || i == 2 || i == 6) // Listbox select
                    {
                        valueInput = isSelected ? $"< {DataProp[i]} >" : $"  {DataProp[i]}  ";
                    }
                    else // Ô text nhập bình thường
                    {
                        valueInput = Inputs[i];
                    }

                    // Đổi màu chữ nếu dòng này đang được chọn (Selected)
                    if (isSelected)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                    }

                    // 2. IN RA CẢ 2 CỘT TRÊN CÙNG MỘT DÒNG ĐÚNG THEO FORMAT
                    // Dùng thêm PadRight để xóa ký tự thừa của lần render trước nếu chuỗi mới ngắn hơn
                    string fullLine = string.Format(format, label, valueInput);
                    Console.WriteLine(fullLine.PadRight(60));
                    Console.WriteLine("".PadRight(10));

                    // Reset lại màu chữ mặc định cho dòng sau
                    Console.ResetColor();
                }

                // VẼ GIAO DIỆN FOOTER
                UILayout.Footer("[F8]: Bấm để Cập Nhật | [ESC]: Thoát ra ngoài", tototalTableWidth);

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                // 1. Điều hướng lên / xuống
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    currentField = (currentField + 1) % Fields.Length;
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    currentField = (currentField - 1 + Fields.Length) % Fields.Length;
                }

                // 2. Thao tác Listbox qua lại (Mũi tên trái / phải)
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    if (currentField == 1 && countGenreList > 0)
                    {
                        _currentGenreIndex = (_currentGenreIndex + 1) % countGenreList;
                        DataProp[1] = GenreList[_currentGenreIndex].GenreName ?? "";
                        Inputs[1] = GenreList[_currentGenreIndex].GenreUuid ?? "";
                    }
                    else if (currentField == 2 && countLanguages > 0)
                    {
                        _currentLanguageIndex = (_currentLanguageIndex + 1) % countLanguages;
                        DataProp[2] = Languages[_currentLanguageIndex].ToString();
                        Inputs[2] = Languages[_currentLanguageIndex].ToString();
                    }
                    else if (currentField == 6 && Status.Count > 0)
                    {
                        _currentStatusIndex = (_currentStatusIndex + 1) % Status.Count;
                        DataProp[6] = Status[_currentStatusIndex].ToString();
                        Inputs[6] = Status[_currentStatusIndex].ToString();
                    }
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    if (currentField == 1 && countGenreList > 0)
                    {
                        _currentGenreIndex = (_currentGenreIndex - 1 + countGenreList) % countGenreList;
                        DataProp[1] = GenreList[_currentGenreIndex].GenreName ?? "";
                        Inputs[1] = GenreList[_currentGenreIndex].GenreUuid ?? "";
                    }
                    else if (currentField == 2 && countLanguages > 0)
                    {
                        _currentLanguageIndex = (_currentLanguageIndex - 1 + countLanguages) % countLanguages;
                        DataProp[2] = Languages[_currentLanguageIndex].ToString();
                        Inputs[2] = Languages[_currentLanguageIndex].ToString();
                    }
                    else if (currentField == 6 && Status.Count > 0)
                    {
                        _currentStatusIndex = (_currentStatusIndex - 1 + Status.Count) % Status.Count;
                        DataProp[6] = Status[_currentStatusIndex].ToString();
                        Inputs[6] = Status[_currentStatusIndex].ToString();
                    }
                }

                // 3. Xử lý nút Xóa (Backspace) cho các trường nhập liệu thông thường
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (currentField != 1 && currentField != 2 && currentField != 6 && Inputs[currentField].Length > 0)
                    {
                        // XỬ LÝ RIÊNG CHO TRƯỜNG NGÀY PHÁT HÀNH (INDEX 3) KHI XÓA
                        // Nếu đang có dạng "dd/" (lên 3 ký tự) hoặc "dd/MM/" (lên 6 ký tự), xóa dấu '/' đồng thời xóa luôn số liền trước

                        if (currentField == 3 && (RELEASEDATELENGHT == 3 || RELEASEDATELENGHT == 6))
                        {
                            Inputs[3] = Inputs[3].Substring(0, RELEASEDATELENGHT - 2);
                        }
                        else
                        {
                            Inputs[currentField] = Inputs[currentField].Substring(0, Inputs[currentField].Length - 1);
                        }
                    }
                }

                // 4. Thoát chương trình
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Console.CursorVisible = true;
                    Console.Clear();
                    await _movieController.List();
                }

                // 5. XỬ LÝ LƯU DỮ LIỆU (F8)
                else if (keyInfo.Key == ConsoleKey.F8)
                {
                    if (Inputs[3] == "" || !Inputs[3].IsValidDateExact("dd/MM/yyyy"))
                    {
                        // In ra dòng lỗi đè cái footer
                        Console.SetCursorPosition(1, 13);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Lỗi: Ngày không hợp lệ hoặc sai định dạng!".PadRight(Console.WindowWidth - 1));
                        Console.ResetColor();

                        // Dừng màn hình cho người dung xem lỗi
                        Console.ReadKey(true);

                        // In đè một hàng khoảng trắng để xóa sạch dòng chữ đỏ cũ trả lại footer
                        Console.SetCursorPosition(1, 13);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("[F8]: Bấm để Thêm Mới | [ESC]: Thoát ra ngoài".PadRight(Console.WindowWidth - 1));
                        Console.ResetColor();

                        // Quay lại vòng lặp bắt người dùng tiếp tục nhập cho đến khi đúng
                        continue;
                    }

                    if (Inputs[0] == "")
                    {
                        // In ra dòng lỗi đè cái footer
                        Console.SetCursorPosition(1, 13);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Lỗi: Không được bỏ trống tên phim!".PadRight(Console.WindowWidth - 1));
                        Console.ResetColor();

                        // Dừng màn hình cho người dung xem lỗi
                        Console.ReadKey(true);

                        // In đè một hàng khoảng trắng để xóa sạch dòng chữ đỏ cũ trả lại footer
                        Console.SetCursorPosition(1, 13);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("[F8]: Bấm để Thêm Mới | [ESC]: Thoát ra ngoài".PadRight(Console.WindowWidth - 1));
                        Console.ResetColor();

                        // Quay lại vòng lặp bắt người dùng tiếp tục nhập cho đến khi đúng
                        continue;
                    }
                    // Trả về controller điều hướng tiếp tục xử lý cập nhật với input mới

                    var movieNew = new Movie
                    {
                        MovieUuid = movie.MovieUuid,
                        MovieName = Inputs[0],
                        GenreUuid = Inputs[1],
                        Language = Enum.TryParse<LanguageEnum>(Inputs[2], out var lang) ? lang : null,
                        ReleaseDate = Inputs[3],
                        Duration = int.TryParse(Inputs[4], out int d) ? d : 0,
                        AgeCustomer = int.TryParse(Inputs[5], out int a) ? a : 0,
                        Status = movie.Status ?? null
                    };
                    await _movieController.UpdateMovie(movieNew);
                    return;
                }

                // 6. Nhập liệu ký tự từ bàn phím chữ/số
                else if ((keyInfo.KeyChar >= 32 && keyInfo.KeyChar <= 126) || keyInfo.KeyChar >= 192)
                {
                    // Giới hạn kí tự tên phim
                    if (currentField == 0)
                    {
                        if (Inputs[0].Length < 45)
                            Inputs[0] += keyInfo.KeyChar;
                    }
                    else if (currentField != 1 && currentField != 2 && currentField != 6)
                    {
                        // XỬ LÝ GÕ CHO TRƯỜNG NGÀY PHÁT HÀNH (INDEX 3)
                        if (currentField == 3)
                        {
                            if (char.IsDigit(keyInfo.KeyChar) && RELEASEDATELENGHT < 10)
                            {
                                Inputs[3] += keyInfo.KeyChar;

                                // Tự động chèn thêm dấu '/' sau khi gõ xong ngày (2 số) hoặc tháng (5 ký tự dạng 'dd/MM')
                                if (RELEASEDATELENGHT == 2 || RELEASEDATELENGHT == 5)
                                {
                                    Inputs[3] += "/";
                                }
                            }
                        }
                        // Ràng buộc nhập thời lượng phim chiếu không quá xxxx
                        else if (currentField == 4)
                        {
                            if (char.IsDigit(keyInfo.KeyChar) && Inputs[4].Length < 4)
                                Inputs[4] += keyInfo.KeyChar;
                        }
                        // Độ tuổi tối thiểu không hơn xx
                        else if (currentField == 5)
                        {
                            if (char.IsDigit(keyInfo.KeyChar) && Inputs[5].Length < 2)
                                Inputs[5] += keyInfo.KeyChar;
                        }
                        else
                        {
                            // Nhập văn bản thông thường
                            if (Inputs[currentField].Length < 25)
                            {
                                Inputs[currentField] += keyInfo.KeyChar;
                            }
                        }
                    }
                }
            }
        }

    }
}