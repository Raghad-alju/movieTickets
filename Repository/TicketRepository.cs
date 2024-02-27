using Microsoft.EntityFrameworkCore;
using movieTickets.data_context;
using movieTickets.Models;
using movieTickets.Repository.IRepository;

namespace movieTickets.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly DataContext _db;
        public TicketRepository(DataContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Ticket ticket)
        {
            _db.Add(ticket);
        }

        public async Task<ICollection<Ticket>> GetAllAsync()
        {
            return await _db.Tickets.ToListAsync();
        }

        public async Task<Ticket> GetAsync(int id)
        {
            return await _db.Tickets.FirstOrDefaultAsync(u => u.TicketId == id);
        }

        public async Task<Ticket> GetAsync(string ticketName)
        {
            return await _db.Tickets.FirstOrDefaultAsync(u => u.TicketNumber.ToLower() == ticketName.ToLower());
        }

        public async Task RemoveAsync(Ticket ticket)
        {
            _db.Tickets.Remove(ticket);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            _db.Tickets.Update(ticket);
        }
    }
}

