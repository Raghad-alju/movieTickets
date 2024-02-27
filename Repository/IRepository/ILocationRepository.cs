using movieTickets.Models;

namespace movieTickets.Repository.IRepository
{
    public interface ILocationRepository
    {
        Task<ICollection<Location>> GetAllAsync();
        Task<Location> GetAsync(int id);
        Task<Location> GetAsync(string locationName);
        Task CreateAsync(Location location);
        Task UpdateAsync(Location location);
        Task RemoveAsync(Location location);
        Task SaveAsync();
    }
}
