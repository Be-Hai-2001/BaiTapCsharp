
using CinemeBooking.Domain.Entities.Movies;

namespace CinemeBooking.Services.Interfaces.Movies;

// Nơi đăng khai báo các phụ thuộc để controller gọi
public interface IMovieService
{
    Task StoreMovieAsync(string[] movie);
    Task UpdateMovieAsync(Movie movie);
    Task<int> DestroyMovieAsync(string movieUuid);
    Task<List<Movie>> ListMovieAsync();
}