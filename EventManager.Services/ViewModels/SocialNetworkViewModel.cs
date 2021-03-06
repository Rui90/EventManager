using System.ComponentModel.DataAnnotations;

namespace EventManager.Services.ViewModels
{
    public class SocialNetworkViewModel
    {
        public int Id { get; set;}

        [Required]
        [MaxLength(512)]
        public string Name { get; set; }

        public string Url { get; set; }
    }
}