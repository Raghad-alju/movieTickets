using movieTickets.Models;

namespace movieTickets.Repository.IRepository
{
    public interface ISeatRepository
    {
        Task<ICollection<Seat>> GetAllAsync();
        Task<Seat> GetAsync(int id);
        Task<Seat> GetAsync(string seatNumber);
        Task CreateAsync(Seat seat);
        Task UpdateAsync(Seat seat);
        Task RemoveAsync(Seat seat);
        Task SaveAsync();
    }
}
