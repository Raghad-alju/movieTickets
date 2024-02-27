using Microsoft.EntityFrameworkCore;
using movieTickets.data_context;
using movieTickets.Models;
using movieTickets.Repository.IRepository;

namespace movieTickets.Repository
{
    public class TimeRepository : ITimeRepository
    {
        private readonly DataContext _db;
        public TimeRepository(DataContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Time time)
        {
            _db.Add(time);
        }

        public async Task<ICollection<Time>> GetAllAsync()
        {
            return await _db.Times.ToListAsync();
        }

        public async Task<Time> GetAsync(int id)
        {
            return await _db.Times.FirstOrDefaultAsync(u => u.TimeId == id);
        }

        public async Task<Time> GetAsync(string availableTime)
        {
            return await _db.Times.FirstOrDefaultAsync(u => u.AvalibaleTime.ToLower() == availableTime.ToLower());
        }

        public async Task RemoveAsync(Time time)
        {
            _db.Times.Remove(time);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Time time)
        {
            _db.Times.Update(time);
        }
    }
}

