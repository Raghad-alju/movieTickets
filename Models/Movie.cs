namespace movieTickets.Models
{
    public class Movie
    {
        public int movieId { get; set; }
        public string Title { get; set; }   
        public string Description { get; set; } 
        public string[] Catogories { get; set; }
        public string Language { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Director { get; set; }
        public string Cast {  get; set; }
        public ICollection<Theater> Theaters { get; set; }
        public ICollection<Experience> Experiences { get; set; }
    }
}
