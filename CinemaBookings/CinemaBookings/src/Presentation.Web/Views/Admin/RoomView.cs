
using CinemaBooking.Presentation.Controllers;
using CinemaBooking.Presentation.Views.Layout;
using CinemaBookings.Views.Admin.Layout;
using CinemeBooking.Domain.Entities.Cinemas;

class RoomView
{
    // Danh sách phòng có trong
    public static void List(List<Room> rooms, RoomController roomController, IServiceProvider provider)
    {

        string formatColumn = "| {0,-5} | {1,-40} | {2,-13} |";
        string lineDivider = "+-------+------------------------------------------+---------------+";
        int roomCount = rooms.Count;
        string inputStt = "";
        int? chosenStt = null;

        while (true)
        {
            Console.Clear();

            // HEADER
            UILayout.Header("DANH SÁCH PHÒNG CHIẾU");

            // BODY
            if (roomCount > 0)
            {
                // Tiêu đề
                Console.WriteLine(lineDivider);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(formatColumn, "STT", "Tên phòng chiếu", "Số lượng ghế");
                Console.ResetColor();
                Console.WriteLine(lineDivider);

                // Danh sách phòng
                for (int index = 0; index < roomCount; index++)
                {
                    string roomName = rooms[index].RoomName ?? string.Empty;
                    if (roomName.Length > 30)
                        roomName = roomName.Substring(0, 27) + "...";

                    Console.WriteLine(formatColumn, index, roomName, rooms[index].TotalSeats);

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(lineDivider);
                    Console.ResetColor();
                }
            }
            else
                // Hiển thị thông báo không có phòng chiếu
                Console.WriteLine("Không có phòng chiếu...");

            // DebugExtensions.dd(rooms.Count >= 0);
            Console.Write("\n> Chọn STT để thao tác: ");
            int cursorLeft = Console.CursorLeft;
            int cursorTop = Console.CursorTop;

            //FOOTER
            UILayout.Footer("[F8]: Thêm Mới Phòng Chiếu - [ESC]: Thoát ra ngoài");

            Console.SetCursorPosition(cursorLeft, cursorTop);

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            // string inputStt = "";
            // int? chosenStt = null;

            if (keyInfo.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (keyInfo.Key == ConsoleKey.F8)
            {
                Console.WriteLine("Chức năng thêm mới");
                Console.ReadKey();
                return;
            }

            // Thao tác người dùng chọn vào "phòng chiếu" 
            else
            {
                // 1. Nếu người dùng nhấn ENTER -> Xử lý số STT đã nhập
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (int.TryParse(inputStt, out int parsedId) && parsedId >= 0 && parsedId < roomCount)
                    {
                        DebugExtensions.dd(parsedId);
                        chosenStt = parsedId;
                        break; // Nhập đúng STT, thoát vòng lặp nhỏ để xử lý Menu Edit/Delete
                    }
                    else
                    {
                        // Báo lỗi sai STT trực tiếp tại dòng nhập
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(" -> Lỗi: STT không hợp lệ!");
                        Console.ResetColor();
                        Thread.Sleep(1200);

                        // Xóa dòng lỗi để người dùng nhập lại
                        Console.SetCursorPosition(cursorLeft, cursorTop);
                        Console.Write(new string(' ', Console.WindowWidth - cursorLeft));
                        Console.SetCursorPosition(cursorLeft, cursorTop);
                        inputStt = "";
                    }
                }
                // 2. Nếu bấm Backspace thì cho phép xóa ký tự vừa gõ
                else if (keyInfo.Key == ConsoleKey.Backspace && inputStt.Length > 0)
                {
                    inputStt = inputStt.Substring(0, inputStt.Length - 1);
                    Console.Write("\b \b"); // Lùi con trỏ và xóa ký tự trên màn hình
                }
                // 3. Nếu nhập số thì hiển thị lên màn hình
                else if (char.IsDigit(keyInfo.KeyChar))
                {
                    inputStt += keyInfo.KeyChar;
                    // DebugExtensions.dd(inputStt);
                    Console.Write(keyInfo.KeyChar);
                    // Console.ReadKey();
                }
            }

            // ================== VẼ TIẾP MENU CHỨC NĂNG (EDIT/DELETE) ================== //
            if (chosenStt != null)
            {

                // Clear bớt phần dưới để vẽ menu phụ
                Console.SetCursorPosition(0, cursorTop + 1);
                Console.WriteLine(new string(' ', Console.WindowWidth * 4)); // Xóa trắng 4 dòng dưới
                Console.SetCursorPosition(0, cursorTop + 1);

                Console.WriteLine($"\n Bạn đã chọn phim: {rooms[chosenStt.Value].RoomName} \n");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(Padding.PadCenter("== [1]:SỬA PHÒNG CHIẾU | [2]: XÓA PHÒNG CHIẾU | [PHÍM KHÁC]: QUAY LẠI DANH SÁCH ==", 85));
                Console.Write(" Chọn: ");
                Console.ResetColor();

                // Lắng nghe lựa chọn menu phụ (tiếp tục dùng ReadKey để không bị kẹt F8/ESC)
                ConsoleKeyInfo subKey = Console.ReadKey(true);

                if (subKey.Key == ConsoleKey.F8) continue; // Nếu lỡ bấm F8 ở đây thì lặp lại để nhảy lên check F8 bên trên
                if (subKey.Key == ConsoleKey.Escape) return;

                switch (subKey.KeyChar)
                {
                    case '1':
                        // await movieController.EditMovie(rooms[chosenStt.Value]);
                        Console.ReadKey();
                        break;
                    case '2':
                        // await movieController.DestroyMovie(rooms[chosenStt.Value].MovieUuid);
                        Console.ReadKey();
                        break;
                }

                Console.ReadKey();
            }

        }
    }
}