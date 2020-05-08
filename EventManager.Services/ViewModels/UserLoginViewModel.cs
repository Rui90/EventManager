using System.ComponentModel.DataAnnotations;

namespace EventManager.Services.ViewModels
{
    public class UserLoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
        
    }
}