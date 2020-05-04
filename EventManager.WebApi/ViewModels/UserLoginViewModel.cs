using System.ComponentModel.DataAnnotations;

namespace EventManager.WebApi.ViewModels
{
    public class UserLoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
        
    }
}