using Microsoft.EntityFrameworkCore;
using movieTickets.data_context;
using movieTickets.Models;
using movieTickets.Repository.IRepository;

namespace movieTickets.Repository
{
    public class TheaterRepository : ITheaterRepository
    {
        private readonly DataContext _db;
        public TheaterRepository(DataContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Theater theater)
        {
            _db.Add(theater);
        }

        public async Task<ICollection<Theater>> GetAllAsync()
        {
            return await _db.Theaters.ToListAsync();
        }

        public async Task<Theater> GetAsync(int id)
        {
            return await _db.Theaters.FirstOrDefaultAsync(u => u.TheaterId == id);
        }

        public async Task<Theater> GetAsync(string theaterName)
        {
            return await _db.Theaters.FirstOrDefaultAsync(u => u.TheaterName.ToLower() == theaterName.ToLower());
        }

        public async Task RemoveAsync(Theater theater)
        {
            _db.Theaters.Remove(theater);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Theater theater)
        {
            _db.Theaters.Update(theater);
        }
    }
}

