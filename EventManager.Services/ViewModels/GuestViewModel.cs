using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventManager.Services.ViewModels
{
    public class GuestViewModel
    {
        public int Id {get;set;}


        [Required]
        [MaxLength(512)]
        public string Name {get; set;}

        [MaxLength(512)]
        public string Description {get; set;}

        public string ImagemUrl {get; set;}

        [Phone] 
        public string PhoneNumber {get; set;}

        [EmailAddress]
        public string Email {get; set;}

        public List<SocialNetworkViewModel> SocialNetworks { get; set;}
        public List<EventViewModel> Events { get; set;}
    }
}