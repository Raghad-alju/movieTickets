namespace movieTickets.Models
{
    public class Experience
    {
        public int ExperienceId { get; set; } 
        public string Name { get; set; }    
        public double Cost { get; set; }
       
        public ICollection<Time> Times { get; set; } 
        public ICollection<MovieExperience> MovieExpereinces { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Seat> Seats { get; set; }
    }
}
