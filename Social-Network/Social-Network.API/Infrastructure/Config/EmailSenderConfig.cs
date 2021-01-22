using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Social_Network.API.Models.EmailOptions;
using System;

namespace Social_Network.API.Infrastructure.Config
{
    public static class EmailSenderConfig
    {
        public static void EmailSender(this IServiceCollection services, 
                                           IConfiguration configuration) 
        {
            var uri = configuration["MailSender:EmailBodyUri"];
            var uriDev = configuration["MailSender:EmailBodyUriDev"];
            services.Configure<EmailConfigModel>(opt => 
            {
                opt.From = configuration["MailSender:From"];
                opt.SmtpServer = configuration["MailSender:SmtpServer"];
                opt.Port = Int32.Parse(configuration["MailSender:Port"]);
                opt.Password = configuration["MailSender:Password"];
                opt.Username = configuration["MailSender:Username"];
                opt.EmailSubject = configuration["MailSender:EmailSubject"];
                opt.EmailBodyText = configuration["MailSender:EmailBodyText"];
                opt.EmailBodyUri = uri;
            });
        }
    }
}
