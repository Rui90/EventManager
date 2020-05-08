using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventManager.Services.ViewModels
{
    public class EventViewModel
    {   
        public int Id {get; set;}

        [Required]

        [StringLength(200, MinimumLength = 5, ErrorMessage = "Local between 5 and 200 characters")]
        public string Location {get; set;}

        public DateTime Date {get;set;}

        [Required (ErrorMessage = "Theme is required.")]
        public string Theme {get;set;}

        [Range(1, 5000000)]
        public int Capacity {get;set;}

        public string ImageUrl { get; set;}

        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email {get; set;}

        public List<LotViewModel> Lots {get; set;}

        public List<SocialNetworkViewModel> SocialNetworks { get; set;}

        public List<GuestViewModel> GuestsEvents { get; set;}
        
    }
}