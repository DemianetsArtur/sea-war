using Microsoft.Azure.Cosmos.Table;
using Social_Network.DAL.Entities;
using Social_Network.DAL.Infrastructure.Interfaces;
using Social_Network.DAL.Infrastructure.Models;
using Social_Network.DAL.Manages.Tables;

namespace Social_Network.DAL.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly TableManage _tableManage;

        public NotificationRepository(TableManage tableManage)
        {
            this._tableManage = tableManage;
        }

        public void NotificationCreate(Notification entity)
        {
            var cloudTable = TableResponse.GetTable(this._tableManage.StorageKey, StorageInfo.NotificationTable);
            var operation = TableOperation.InsertOrReplace(entity);
            cloudTable.Execute(operation);
        }
    }
}