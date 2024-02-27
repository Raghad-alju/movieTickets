using Microsoft.EntityFrameworkCore;
using movieTickets.data_context;
using movieTickets.Models;
using movieTickets.Repository.IRepository;

namespace movieTickets.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly DataContext _db;
        public LocationRepository(DataContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Location movie)
        {
            _db.Add(movie);
        }

        public async Task<ICollection<Location>> GetAllAsync()
        {
            return await _db.Locations.ToListAsync();
        }

        public async Task<Location> GetAsync(int id)
        {
            return await _db.Locations.FirstOrDefaultAsync(u => u.LocationId == id);
        }

        public async Task<Location> GetAsync(string locationName)
        {
            return await _db.Locations.FirstOrDefaultAsync(u => u.City.ToLower() == locationName.ToLower());
        }

        public async Task RemoveAsync(Location location)
        {
            _db.Locations.Remove(location);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Location location)
        {
            _db.Locations.Update(location);
        }
    }
}

