using Microsoft.EntityFrameworkCore;
using movieTickets.data_context;
using movieTickets.Models;
using movieTickets.Repository.IRepository;
using static Azure.Core.HttpHeader;

namespace movieTickets.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly DataContext _db;
        public MovieRepository(DataContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Movie movie)
        {
            _db.Add(movie);
        }

        public async Task<ICollection<Movie>> GetAllAsync()
        {
            return await _db.Movies.ToListAsync();
        }

        public async Task<Movie> GetAsync(int id)
        {
            return await _db.Movies.FirstOrDefaultAsync(u => u.MovieId == id);
        }

        public async Task<Movie> GetAsync(string movieTitle)
        {
            return await _db.Movies.FirstOrDefaultAsync(u => u.Title.ToLower() == movieTitle.ToLower());
        }

        public async Task RemoveAsync(Movie movie)
        {
            _db.Movies.Remove(movie);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Movie movie)
        {
            _db.Movies.Update(movie);
        }
    }
}
