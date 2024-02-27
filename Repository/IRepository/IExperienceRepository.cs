using movieTickets.Models;

namespace movieTickets.Repository.IRepository
{
    public interface IExperienceRepository
    {

        Task<ICollection<Experience>> GetAllAsync();
        Task<Experience> GetAsync(int id);
        Task<Experience> GetAsync(string experienceName);
        Task CreateAsync(Experience experience);
        Task UpdateAsync(Experience experience);
        Task RemoveAsync(Experience experience);
        Task SaveAsync();
    }
}
