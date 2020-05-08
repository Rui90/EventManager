using System.ComponentModel.DataAnnotations;

namespace EventManager.Services.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [MaxLength(512)]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        
    }
}