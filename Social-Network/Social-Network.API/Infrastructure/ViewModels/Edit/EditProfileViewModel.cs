using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Social_Network.API.Infrastructure.ViewModels.Edit
{
    public class EditProfileViewModel
    {
        [Required] public string Name { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AboutMe { get; set; }

        public string Date { get; set; }

        public string ImagePath { get; set; }

        public string ContentName { get; set; }

        public IFormFile Content { get; set; }
    }
}
