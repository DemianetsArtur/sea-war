using Azure.Storage.Blobs;
using Social_Network.DAL.Infrastructure.Interfaces;
using Social_Network.DAL.Infrastructure.Repositories;
using Social_Network.DAL.Manages.Tables;

namespace Social_Network.DAL.Infrastructure.RepositoryManage
{
    public class UoW : IUoW
    {
        private readonly TableManage _tableManage;
        private readonly BlobServiceClient _blobServiceClient;

        public IUserAccountRepository UserAccount { get; }
        public IBlobStorageRepository BlobStorage { get; }
        public IFriendRepository Friend { get; }
        public INotificationRepository Notification { get; }

        public UoW(TableManage tableManage, BlobServiceClient blobServiceClient)
        {
            this._tableManage = tableManage;
            this._blobServiceClient = blobServiceClient;
            this.UserAccount = new UserAccountRepository(this._tableManage);
            this.BlobStorage = new BlobStorageRepository(this._blobServiceClient);
            this.Friend = new FriendRepository(this._tableManage);
            this.Notification = new NotificationRepository(this._tableManage);
        }
    }
}
