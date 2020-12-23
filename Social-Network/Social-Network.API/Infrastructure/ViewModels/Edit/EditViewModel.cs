using System.ComponentModel.DataAnnotations;

namespace Social_Network.API.Infrastructure.ViewModels.Edit
{
    public class EditViewModel
    {
        [Required] public string Name { get; set; }
        
        [Required] public string Email { get; set; }
        
        [Required] public string FirstName {get;set;}
        
        [Required] public string LastName {get;set;}

        [Required] public string AboutMe { get; set; }

        [Required] public string Date { get; set; }
        
                   public string ImagePath { get; set; }
        [Required] public string ChangedName { get; set; }
    }
}