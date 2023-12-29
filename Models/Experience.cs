namespace movieTickets.Models
{
    public class Experience
    {
        public int experienceId { get; set; } 
        public string Name { get; set; }    
        public double Cost { get; set; }
        public string Times { get; set; }
        
        public string MovieName { get; set; }
        public ICollection<Movie> Movies { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
