using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using movieTickets.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace movieTickets.data_context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
        
        }

        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Geners { get; set; }
        public DbSet<Time> Times { get; set; }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Theater> Theaters { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<MovieExperience> MovieExperiences { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Theater> MovieTheaters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MovieExperience>()
                  .HasKey(e => new { e.MovieID, e.ExperienceID });
            modelBuilder.Entity<MovieExperience>()
                    .HasOne(e => e.Experience)
                    .WithMany(e => e.MovieExpereinces)
                    .HasForeignKey(e => e.ExperienceID);
            modelBuilder.Entity<MovieExperience>()
                    .HasOne(m => m.Movie)
                    .WithMany(m => m.MovieExperiences)
                    .HasForeignKey(m => m.MovieID);


            modelBuilder.Entity<MovieTheater>()
                  .HasKey(e => new { e.MovieID, e.TheaterID });
            modelBuilder.Entity<MovieTheater>()
                    .HasOne(t => t.Theater)
                    .WithMany(t => t.MovieTheaters)
                    .HasForeignKey(t => t.TheaterID);
            modelBuilder.Entity<MovieTheater>()
                    .HasOne(m => m.Movie)
                    .WithMany(m => m.MovieTheaters)
                    .HasForeignKey(m => m.MovieID);
        }

    }
}
