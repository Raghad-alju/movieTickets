using Microsoft.EntityFrameworkCore;

namespace movieTickets.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }   
        public string Description { get; set; }
        
        public ICollection<Genre> Geners { get; set; }
        public string Language { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Director { get; set; }
        public string Cast {  get; set; }
        public ICollection<MovieTheater> MovieTheaters { get; set; }
        public ICollection<MovieExperience> MovieExperiences { get; set; }
    }
}
