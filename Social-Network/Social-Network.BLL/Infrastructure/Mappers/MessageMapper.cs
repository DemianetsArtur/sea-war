using AutoMapper;
using Social_Network.BLL.ModelsDto;
using Social_Network.DAL.Entities;

namespace Social_Network.BLL.Infrastructure.Mappers
{
    public class MessageMapper : Profile
    {
        public MessageMapper()
        {
            this.CreateMap<Message, MessageDto>();
            this.CreateMap<MessageDto, Message>();
        }
    }
}