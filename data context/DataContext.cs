using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using movieTickets.Models;

namespace movieTickets.data_context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
        
        }

        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Theater> theaters { get; set; }
        public DbSet<Ticket> tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Experience>().
             HasMany(e => e.Movies)
            .WithMany(e => e.Experiences);
        }

    }
}
