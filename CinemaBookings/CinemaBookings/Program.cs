using CinemeBooking.Views;

class Program
{
    static void Main(string[] args)
    {
        // 1. Khởi tạo các tầng repository / service (Ví dụ)
        // IMovieService movieService = new MovieService();

        // 2. Gọi hiển thị Menu
        MainMenuView.Show();
    }
}