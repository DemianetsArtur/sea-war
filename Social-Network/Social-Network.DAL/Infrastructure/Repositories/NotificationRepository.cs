using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public void EventAddToFriend(Notification entity)
        {
            entity.NameResponse = NotificationInfo.EventAddToFriend;
            this.NotificationCreate(entity);
        }

        public ICollection<Notification> GetEventAddToFriend()
        {
            var cloudTable = TableResponse.GetTable(this._tableManage.StorageKey, StorageInfo.NotificationTable);
            var query = new TableQuery<Notification>();
            var entitiesTable = cloudTable.ExecuteQuery(query).ToList();
            return entitiesTable;
        }

        public void EventAddToFriendRemove(Notification entity)
        {
            var userNameResponseQuery =
                TableQuery.GenerateFilterCondition("UserNameResponse", QueryComparisons.Equal, entity.UserNameResponse);
            var userNameToResponseQuery = 
                TableQuery.GenerateFilterCondition("UserNameToResponse", QueryComparisons.Equal, entity.UserNameToResponse);
            var nameResponseQuery = 
                TableQuery.GenerateFilterCondition("NameResponse", QueryComparisons.Equal, entity.NameResponse);
            var usersNameCombineFilter =
                TableQuery.CombineFilters(userNameResponseQuery, TableOperators.And, userNameToResponseQuery);
            var combineQuery = 
                TableQuery.CombineFilters(userNameToResponseQuery, TableOperators.And, usersNameCombineFilter);
            var query = new TableQuery<Notification>().Where(combineQuery);
            var cloudTable = TableResponse.GetTable(this._tableManage.StorageKey, StorageInfo.NotificationTable);
            var entityTable = cloudTable.ExecuteQuery(query).FirstOrDefault();
            var operation = TableOperation.Delete(entityTable);
            cloudTable.Execute(operation);
        }

        private void NotificationCreate(Notification entity)
        {
            var cloudTable = TableResponse.GetTable(this._tableManage.StorageKey, StorageInfo.NotificationTable);
            var operation = TableOperation.InsertOrReplace(entity);
            cloudTable.Execute(operation);
        }
    }
}