﻿namespace Social_Network.DAL.Infrastructure.Interfaces
{
    public interface IUoW
    {
        IUserAccountRepository UserAccount { get; }
        IBlobStorageRepository BlobStorage { get; }
        IFriendRepository Friend { get; }
        INotificationRepository Notification { get; }
    }
}
