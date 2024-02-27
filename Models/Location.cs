namespace movieTickets.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        public string City { get; set; }
        public ICollection<Theater> Theaters { get; set;}
    }
}
