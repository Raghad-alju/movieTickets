namespace movieTickets.Models
{
    public class Theater
    {
        public int TheaterId { get; set; }
        public string TheaterName { get; set; }
        public Location Location { get; set; }
        public ICollection<MovieTheater> MovieTheaters { get; set; }


    }
}
