using System.Collections.Generic;
using Social_Network.BLL.ModelsDto;

namespace Social_Network.BLL.Infrastructure.Interfaces
{
    public interface IMessageService
    {
        void MessageCreate(MessageDto entity);
        ICollection<MessageDto> MessageAll(MessageDto entity);
    }
}