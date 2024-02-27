using movieTickets.Models;
using static Azure.Core.HttpHeader;

namespace movieTickets.Repository.IRepository
{
    public interface IMovieRepository
    {

        Task<ICollection<Movie>> GetAllAsync();
        Task<Movie> GetAsync(int id);
        Task<Movie> GetAsync(string movieName);
        Task CreateAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        Task RemoveAsync(Movie movie);
        Task SaveAsync();
    }
}
