using Microsoft.EntityFrameworkCore;
using movieTickets.data_context;
using movieTickets.Models;
using movieTickets.Repository.IRepository;

namespace movieTickets.Repository
{
    public class GenreRepository :IGenreRepository
    {
        private readonly DataContext _db;
        public GenreRepository(DataContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Genre genre)
        {
            _db.Add(genre);
        }

        public async Task<ICollection<Genre>> GetAllAsync()
        {
            return await _db.Geners.ToListAsync();
        }

        public async Task<Genre> GetAsync(int id)
        {
            return await _db.Geners.FirstOrDefaultAsync(u => u.GenreId == id);
        }

        public async Task<Genre> GetAsync(string genreName)
        {
            return await _db.Geners.FirstOrDefaultAsync(u => u.GenreName.ToLower() == genreName.ToLower());
        }

        public async Task RemoveAsync(Genre genre)
        {
            _db.Geners.Remove(genre);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Genre genre)
        {
            _db.Geners.Update(genre);
        }
    }
}
