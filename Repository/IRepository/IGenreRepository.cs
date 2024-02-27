using movieTickets.Models;

namespace movieTickets.Repository.IRepository
{
    public interface IGenreRepository
    {
        Task<ICollection<Genre>> GetAllAsync();
        Task<Genre> GetAsync(int id);
        Task<Genre> GetAsync(string genreName);
        Task CreateAsync(Genre genre);
        Task UpdateAsync(Genre genre);
        Task RemoveAsync(Genre genre);
        Task SaveAsync();
    }
}
