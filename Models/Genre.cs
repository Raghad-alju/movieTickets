namespace movieTickets.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string GenreName { get; set;}
        public Movie Movie { get; set; }    
    }
}
