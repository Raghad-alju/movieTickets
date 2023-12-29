namespace movieTickets.Models
{
    public class MovieExperience
    {
        public int movieID { get; set; }
        public int experienceID { get; set; }
        public Movie Movie { get; set; }
        public Experience experience { get; set; }
    }
}
