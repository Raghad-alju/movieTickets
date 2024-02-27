namespace movieTickets.Models
{
    public class MovieExperience
    {
        public int MovieID { get; set; }
        public int ExperienceID { get; set; }
        public Movie Movie { get; set; }
        public Experience Experience { get; set; }
    }
}
