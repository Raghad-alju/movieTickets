using movieTickets.Models;

namespace movieTickets.Repository.IRepository
{
    public interface ITheaterRepository
    {
        Task<ICollection<Theater>> GetAllAsync();
        Task<Theater> GetAsync(int id);
        Task<Theater> GetAsync(string theaterName);
        Task CreateAsync(Theater theater);
        Task UpdateAsync(Theater theater);
        Task RemoveAsync(Theater theater);
        Task SaveAsync();
    }
}
