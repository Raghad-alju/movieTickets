using movieTickets.Models;

namespace movieTickets.Repository.IRepository
{
    public interface ITicketRepository
    {
        Task<ICollection<Ticket>> GetAllAsync();
        Task<Ticket> GetAsync(int id);
        Task<Ticket> GetAsync(string ticketName);
        Task CreateAsync(Ticket ticket);
        Task UpdateAsync(Ticket ticket);
        Task RemoveAsync(Ticket ticket);
        Task SaveAsync();
    }
}
