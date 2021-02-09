using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Cosmos.Table;
using Social_Network.DAL.Entities;
using Social_Network.DAL.Infrastructure.Interfaces;
using Social_Network.DAL.Infrastructure.Models;
using Social_Network.DAL.Manages.Tables;

namespace Social_Network.DAL.Infrastructure.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        private readonly TableManage _tableManage;

        public FriendRepository(TableManage tableManage)
        {
            this._tableManage = tableManage;
        }

        public ICollection<UserAccount> FriendAll(string name)
        {
            var query = new TableQuery<UserAccount>()
                .Where(TableQuery.GenerateFilterCondition(TableQueries.NameQuery, QueryComparisons.NotEqual, name));
            var cloudTable = TableResponse.GetTable(this._tableManage.StorageKey, StorageInfo.UserAccountTable);
            var entitiesTable = cloudTable.ExecuteQuery(query).ToList();
            return entitiesTable;
        }

        public ICollection<Friend> UsersInFriendship()
        {
            var cloudTable = TableResponse.GetTable(this._tableManage.StorageKey, StorageInfo.FriendTable);
            var query = new TableQuery<Friend>();
            var entitiesTable = cloudTable.ExecuteQuery(query).ToList();
            return entitiesTable;
        }

        public void UserAddToFriends(Friend entity)
        {
            var cloudTable = TableResponse.GetTable(this._tableManage.StorageKey, StorageInfo.FriendTable);
            var operation = TableOperation.InsertOrReplace(entity);
            cloudTable.Execute(operation);
        }

        public void UserInFriendshipRemove(Friend entity) 
        {
            var userNameResponseQuery =
                TableQuery.GenerateFilterCondition(TableQueries.UserNameResponseQuery, QueryComparisons.Equal, entity.UserNameResponse);
            var userNameToResponseQuery =
                TableQuery.GenerateFilterCondition(TableQueries.UserNameToResponseQuery, QueryComparisons.Equal, entity.UserNameToResponse);
            var combineQuery =
                TableQuery.CombineFilters(userNameResponseQuery, TableOperators.And, userNameToResponseQuery);
            var query = new TableQuery<Friend>().Where(combineQuery);
            var cloudTable = TableResponse.GetTable(this._tableManage.StorageKey, StorageInfo.FriendTable);
            var entityTable = cloudTable.ExecuteQuery(query).FirstOrDefault();
            var operation = TableOperation.Delete(entityTable);
            cloudTable.Execute(operation);
        }

    }
}