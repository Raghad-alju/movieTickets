using Microsoft.EntityFrameworkCore;
using movieTickets.data_context;
using movieTickets.Models;
using movieTickets.Repository.IRepository;

namespace movieTickets.Repository
{
    public class SeatRepository : ISeatRepository
    {
        private readonly DataContext _db;
        public SeatRepository(DataContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Seat seat)
        {
            _db.Add(seat);
        }

        public async Task<ICollection<Seat>> GetAllAsync()
        {
            return await _db.Seats.ToListAsync();
        }

        public async Task<Seat> GetAsync(int id)
        {
            return await _db.Seats.FirstOrDefaultAsync(u => u.SeatId == id);
        }

        public async Task<Seat> GetAsync(string seatNumber)
        {
            return await _db.Seats.FirstOrDefaultAsync(u => u.SeatNumber.ToLower() == seatNumber.ToLower());
        }

        public async Task RemoveAsync(Seat seat)
        {
            _db.Seats.Remove(seat);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seat seat)
        {
            _db.Seats.Update(seat);
        }
    }
}
