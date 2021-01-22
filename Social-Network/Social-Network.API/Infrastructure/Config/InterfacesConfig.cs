using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Social_Network.API.Infrastructure.Manages.MailSender;
using Social_Network.BLL.Infrastructure.Interfaces;
using Social_Network.BLL.Services;
using Social_Network.DAL.Infrastructure.Interfaces;
using Social_Network.DAL.Infrastructure.Repositories;
using Social_Network.DAL.Infrastructure.RepositoryManage;
using Social_Network.DAL.Manages.Tables;

namespace Social_Network.API.Infrastructure.Config
{
    public static class InterfacesConfig
    {
        public static void SetInterfaceDi(this IServiceCollection services, 
                                               IConfiguration configuration) 
        {
            var connectionString = configuration["ConnectionString"];
            var storageAccount = configuration["Table:StorageAccount"];

            services.AddSingleton<IUserAccountRepository>(opt => new UserAccountRepository(new TableManage(storageAccount, connectionString)));
            services.AddSingleton<IBlobStorageRepository, BlobStorageRepository>();
            services.AddSingleton<IFriendRepository>(opt => new FriendRepository(new TableManage(storageAccount, connectionString)));
            services.AddSingleton<INotificationRepository>(_ => new NotificationRepository(new TableManage(storageAccount, connectionString)));
            services.AddSingleton<IMessageRepository>(_ => new MessageRepository(new TableManage(storageAccount, connectionString)));
            services.AddSingleton<IUoW>(_ => new UoW(new TableManage(storageAccount, connectionString), new BlobServiceClient(connectionString)));
            services.AddSingleton<IUserAccountService, UserAccountService>();
            services.AddSingleton<IBlobStorageService, BlobStorageService>();
            services.AddSingleton<IFriendService, FriendService>();
            services.AddSingleton<INotificationService, NotificationService>();
            services.AddSingleton<IMessageService, MessageService>();
            services.AddSingleton<IMailSender, MailSender>();
        } 
    }
}
