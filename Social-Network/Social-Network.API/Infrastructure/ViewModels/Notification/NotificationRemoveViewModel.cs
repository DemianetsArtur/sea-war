using System.ComponentModel.DataAnnotations;

namespace Social_Network.API.Infrastructure.ViewModels.Notification
{
    public class NotificationRemoveViewModel
    {
        [Required] public string UserNameResponse { get; set; }
        [Required] public string UserNameToResponse { get; set; }
        [Required] public string NameResponse { get; set; }
    }
}