using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace Social_Network.API.Models.EmailOptions
{
    public class EmailMessage
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public EmailMessage(IEnumerable<string> to, 
                            string subject, 
                            string content)
        {
            this.To = new List<MailboxAddress>();
            this.To.AddRange(to.Select(opt => new MailboxAddress(opt)));
            this.Subject = subject;
            this.Content = content;
        }
    }
}
