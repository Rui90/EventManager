using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManager.Domain
{
    public class Event
    {
        public int Id {get; set;}

        public string Location {get; set;}

        public DateTime Date {get;set;}

        public string Theme {get;set;}

        [Column(TypeName = "decimal(18,4)")]
        public decimal Capacity {get;set;}

        public string ImageUrl { get; set;}

        public string PhoneNumber { get; set; }

        public string Email {get; set;}

        public List<Lot> Lots {get; set;}

        public List<SocialNetwork> SocialNetworks { get; set;}

        public List<GuestEvent> GuestsEvents { get; set;}
    }
}