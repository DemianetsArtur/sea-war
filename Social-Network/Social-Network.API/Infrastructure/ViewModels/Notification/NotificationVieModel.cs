using System.ComponentModel.DataAnnotations;

namespace Social_Network.API.Infrastructure.ViewModels.Notification
{
    public class NotificationVieModel
    {
        [Required] public string UserNameResponse { get; set; }
        [Required] public string UserNameToResponse { get; set; }
    }
}