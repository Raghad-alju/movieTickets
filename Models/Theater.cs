namespace movieTickets.Models
{
    public class Theater
    {
        public int theaterId { get; set; }
        public int expID { get; set; }
        public string City { get; set; }
        public ICollection<Movie> Movies { get; set; }

    }
}
