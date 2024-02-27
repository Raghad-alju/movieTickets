namespace movieTickets.Models.DTO
{
    public class MovieDTOupdate
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Director { get; set; }
        public string Cast { get; set; }
        
    }
}
