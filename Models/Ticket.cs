namespace movieTickets.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public string TicketNumber { get; set; }
        public string Date { get; set; }
        public Experience Experience { get; set; }
    }
}
