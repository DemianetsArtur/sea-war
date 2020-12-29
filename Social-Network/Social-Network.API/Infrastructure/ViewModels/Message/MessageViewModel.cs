using System.ComponentModel.DataAnnotations;

namespace Social_Network.API.Infrastructure.ViewModels.Message
{
    public class MessageViewModel
    {
        [Required] public string UserNameResponse { get; set; }
        [Required] public string UserNameToResponse { get; set; }
        [Required] public string UserImageResponse { get; set; }
        [Required] public string UserImageToResponse { get; set; }
        [Required] public string Text { get; set; }
    }
}