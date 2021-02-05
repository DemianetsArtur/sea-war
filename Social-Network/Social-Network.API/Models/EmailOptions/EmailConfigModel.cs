
namespace Social_Network.API.Models.EmailOptions
{
    public class EmailConfigModel
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBodyText { get; set; }
        public string EmailBodyUri { get; set; }
        public string EmailBodyUriDev { get; set; }
    }
}
