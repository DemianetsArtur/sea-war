using Social_Network.BLL.ModelsDto;

namespace Social_Network.API.Infrastructure.Manages.MailSender
{
    public interface IMailSender
    {
        void SendEmail(UserAccountDto entity);
    }
}
