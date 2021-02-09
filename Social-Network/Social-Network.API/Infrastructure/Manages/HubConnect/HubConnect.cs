using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Social_Network.BLL.Infrastructure.Interfaces;

namespace Social_Network.API.Infrastructure.Manages.HubConnect
{
    public class HubConnect : Hub
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IFriendService _friendService;
        private readonly IUserAccountService _userAccountService;
        private readonly ICommentService _commentService;

        public HubConnect(IConfiguration configuration, 
                          IMapper mapper, 
                          INotificationService notificationService, 
                          IFriendService friendService, 
                          IUserAccountService userAccountService, 
                          ICommentService commentService)
        {
            this._notificationService = notificationService;
            this._configuration = configuration;
            this._mapper = mapper;
            this._friendService = friendService;
            this._userAccountService = userAccountService;
            this._commentService = commentService;
        }

        public override async Task OnConnectedAsync()
        {
            await this.GetEventAddToFriends();
            await this.GetUsersInFriendship();
            await this.GetUserAll();
            await this.GetCommentPost();
            await base.OnConnectedAsync();
        }

        private async Task GetEventAddToFriends()
        {
            var getEventAddToFriends = this._notificationService.GetEventAddToFriend();
            var nameResponse = this._configuration["HubInfo:GetEventAddToFriends"];
            await this.Clients.All.SendAsync(nameResponse, getEventAddToFriends);
        }

        private async Task GetUserAll()
        {
            await Task.Delay(5000);
            var userAll = this._userAccountService.UserAll();
            var nameResponse = "userAllHub";
            await this.Clients.All.SendAsync(nameResponse, userAll);
        }

        private async Task GetCommentPost() 
        {
            var comments = this._commentService.CommentPostsGetAll();
            var nameResponse = this._configuration["HubInfo:GetCommentPost"];
            await this.Clients.All.SendAsync(nameResponse, comments);
        }

        private async Task GetUsersInFriendship()
        {
            var getUsersInFriendship = this._friendService.UsersInFriendship();
            var nameResponse = this._configuration["HubInfo:GetUsersInFriendship"];
            await this.Clients.All.SendAsync(nameResponse, getUsersInFriendship);
        }
    }
}