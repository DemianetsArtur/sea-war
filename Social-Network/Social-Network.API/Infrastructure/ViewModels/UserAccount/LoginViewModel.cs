using System.ComponentModel.DataAnnotations;

namespace Social_Network.API.Infrastructure.ViewModels.UserAccount
{
    public class LoginViewModel
    {
        [Required] public string Name { get; set; }
        
        [Required] public string Password { get; set; }
    }
}