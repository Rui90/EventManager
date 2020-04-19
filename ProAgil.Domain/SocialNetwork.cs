namespace ProAgil.Domain
{
    public class SocialNetwork
    {
        public int Id { get; set;}

        public string Name { get; set; }

        public string Url { get; set; }

        public int? EventId { get; set; }

        public Event Event { get; }

        public int? GuestId { get; set; }

        public Guest Guest { get; }
    }
}