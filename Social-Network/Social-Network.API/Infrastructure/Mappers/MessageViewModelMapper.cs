using AutoMapper;
using Social_Network.API.Infrastructure.ViewModels.Message;
using Social_Network.BLL.ModelsDto;

namespace Social_Network.API.Infrastructure.Mappers
{
    public class MessageViewModelMapper : Profile
    {
        public MessageViewModelMapper()
        {
            this.CreateMap<MessageViewModel, MessageDto>();
            this.CreateMap<MessageDto, MessageViewModel>();
            
            this.CreateMap<MessageGetViewModel, MessageDto>();
            this.CreateMap<MessageDto, MessageGetViewModel>();
        }
    }
}