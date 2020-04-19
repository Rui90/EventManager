namespace ProAgil.Domain
{
    public class GuestEvent
    {
        public int GuestId { get; set; }
        
        public Guest Guest { get; set; }

        public int EventId { get; set; }

        public Event Event { get; set;}
    }
}