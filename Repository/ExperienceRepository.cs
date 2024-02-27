using Microsoft.EntityFrameworkCore;
using movieTickets.data_context;
using movieTickets.Models;
using movieTickets.Repository.IRepository;

namespace movieTickets.Repository
{
    public class ExperienceRepository : IExperienceRepository
    {
        private readonly DataContext _db;
        public ExperienceRepository(DataContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Experience experience)
        {
            _db.Add(experience);
        }

        public async Task<ICollection<Experience>> GetAllAsync()
        {
            return await _db.Experiences.ToListAsync();
        }

        public async Task<Experience> GetAsync(int id)
        {
            return await _db.Experiences.FirstOrDefaultAsync(u => u.ExperienceId == id);
        }

        public async Task<Experience> GetAsync(string experienceName)
        {
            return await _db.Experiences.FirstOrDefaultAsync(u => u.Name.ToLower() == experienceName.ToLower());
        }

        public async Task RemoveAsync(Experience experience)
        {
            _db.Experiences.Remove(experience);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Experience experience)
        {
            _db.Experiences.Update(experience);
        }
    }
}
