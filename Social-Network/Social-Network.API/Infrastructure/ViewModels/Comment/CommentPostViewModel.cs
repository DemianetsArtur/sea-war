using System.ComponentModel.DataAnnotations;

namespace Social_Network.API.Infrastructure.ViewModels.Comment
{
    public class CommentPostViewModel
    {
        [Required] public string UserName { get; set; }
        [Required] public string UserImage { get; set; }
        [Required] public string UserNameResponse { get; set; }
        [Required] public string Text { get; set; }
        [Required] public string ContentName { get; set; }
    }
}
