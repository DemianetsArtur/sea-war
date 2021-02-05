using MailKit.Net.Smtp;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using MimeKit;
using Social_Network.API.Models.EmailOptions;
using Social_Network.BLL.ModelsDto;
using System.Collections.Generic;

namespace Social_Network.API.Infrastructure.Manages.MailSender
{
    public class MailSender : IMailSender
    {
        public EmailConfigModel _emailConfig { get; set; }
        public MailSender(IOptions<EmailConfigModel> emailConfig)
        {
            this._emailConfig = emailConfig.Value;
        }

        public void SendEmail(UserAccountDto entity) 
        {
            var param = new Dictionary<string, string> { { "token", entity.EmailKey }, { "email", entity.Name } };
            var callback = QueryHelpers.AddQueryString(this._emailConfig.EmailBodyUri, param);
            var emailBody = this._emailConfig.EmailBodyText + callback;
            var message = new EmailMessage(new string[] { entity.Email }, this._emailConfig.EmailSubject , emailBody);
            var emailMessage = this.CreateEmailMessage(message);
            this.Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(EmailMessage message) 
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(this._emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            return emailMessage;
        }

        private void Send(MimeMessage mailMessage) 
        {
            using (var client = new SmtpClient()) 
            {
                try
                {
                    client.Connect(this._emailConfig.SmtpServer, this._emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(this._emailConfig.Username, this._emailConfig.Password);
                    client.Send(mailMessage);
                }
                catch
                {
                    throw;
                }
                finally 
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
