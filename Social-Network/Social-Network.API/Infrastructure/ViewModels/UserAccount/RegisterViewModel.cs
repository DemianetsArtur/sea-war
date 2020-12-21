using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Social_Network.API.Infrastructure.ViewModels.UserAccount
{
    public class RegisterViewModel
    {
        [Required] public string Name { get; set; }
        
        [Required] public string Email { get; set; }
        
        [Required] public string Password { get; set; }
        
        [Required] public string FirstName {get;set;}
        
        [Required] public string LastName {get;set;}

        [Required] public string AboutMe { get; set; }
        
        [Required] public string UserType { get; set; }

    }
}