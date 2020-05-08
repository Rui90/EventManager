using System.Collections.Generic;

namespace EventManager.Domain
{
    public class Guest
    {
        public int Id {get;set;}

        public string Name {get; set;}

        public string Description {get; set;}

        public string ImagemUrl {get; set;}

        public string PhoneNumber {get; set;}

        public string Email {get; set;}

        public List<SocialNetwork> SocialNetworks { get; set;}

        public List<GuestEvent> GuestsEvents { get; set;}
    }
}