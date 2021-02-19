﻿using Azure.Storage.Blobs;
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
        public IMessageRepository Message { get; }
        public INotificationRepository Notification { get; }
        public IPostRepository Post { get; }
        public ICommentRepository Comment { get; }
        public IEditRepository Edit { get; }

        public UoW(TableManage tableManage, 
                   BlobServiceClient blobServiceClient)
        {
            this._tableManage = tableManage;
            this._blobServiceClient = blobServiceClient;

            this.UserAccount = new UserAccountRepository(this._tableManage);
            this.BlobStorage = new BlobStorageRepository(this._blobServiceClient);
            this.Friend = new FriendRepository(this._tableManage);
            this.Notification = new NotificationRepository(this._tableManage);
            this.Message = new MessageRepository(this._tableManage);
            this.Post = new PostRepository(this._tableManage, this._blobServiceClient);
            this.Comment = new CommentRepository(this._tableManage);
            this.Edit = new EditRepository(this._tableManage, this._blobServiceClient);
        }
    }
}
