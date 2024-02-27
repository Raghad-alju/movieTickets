namespace movieTickets.Models
{
    public class MovieTheater
    {
        public int MovieID { get; set; }
        public int TheaterID { get; set; }
        public Movie Movie { get; set; }
        public Theater Theater { get; set;}
    }
}
