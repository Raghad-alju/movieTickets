using movieTickets.Models;

namespace movieTickets.Repository.IRepository
{
    public interface ITimeRepository
    {
        Task<ICollection<Time>> GetAllAsync();
        Task<Time> GetAsync(int id);
        Task<Time> GetAsync(string availableTime);
        Task CreateAsync(Time time);
        Task UpdateAsync(Time time);
        Task RemoveAsync(Time time);
        Task SaveAsync();
    }
}
