using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Cosmos.Table;
using Social_Network.DAL.Entities;
using Social_Network.DAL.Infrastructure.Interfaces;
using Social_Network.DAL.Infrastructure.Models;
using Social_Network.DAL.Manages.Tables;

namespace Social_Network.DAL.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly TableManage _tableManage;

        public MessageRepository(TableManage tableManage)
        {
            this._tableManage = tableManage;
        }

        public void MessageCreate(Message entity)
        {
            var cloudTable = TableResponse.GetTable(this._tableManage.StorageKey, StorageInfo.MessageTable);
            var operation = TableOperation.InsertOrReplace(entity);
            cloudTable.Execute(operation);
        }

        public ICollection<Message> MessageAll(Message entity)
        {
            var userNameResponseQuery =
                TableQuery.GenerateFilterCondition("UserNameResponse", QueryComparisons.Equal, entity.UserNameResponse);
            var userNameToResponseQuery = 
                TableQuery.GenerateFilterCondition("UserNameToResponse", QueryComparisons.Equal, entity.UserNameToResponse);
            var userNameReverseResponseQuery = 
                TableQuery.GenerateFilterCondition("UserNameToResponse", QueryComparisons.Equal, entity.UserNameResponse);
            var userNameReverseToResponseQuery = 
                TableQuery.GenerateFilterCondition("UserNameResponse", QueryComparisons.Equal, entity.UserNameToResponse);
            
            var usersNameCombineFilter =
                TableQuery.CombineFilters(userNameResponseQuery, TableOperators.And, userNameToResponseQuery);
            var userNameReverseCombineFilter =
                TableQuery.CombineFilters(userNameReverseResponseQuery, TableOperators.And, userNameReverseToResponseQuery);
            
            var combineQuery = 
                TableQuery.CombineFilters(usersNameCombineFilter, TableOperators.Or, userNameReverseCombineFilter);
            var query = new TableQuery<Message>().Where(combineQuery);
            var cloudTable = TableResponse.GetTable(this._tableManage.StorageKey, StorageInfo.MessageTable);
            var entityTable = cloudTable.ExecuteQuery(query).ToList();
            return entityTable;
        }
    }
}