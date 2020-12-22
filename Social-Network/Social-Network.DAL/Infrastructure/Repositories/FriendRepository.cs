using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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
                .Where(TableQuery.GenerateFilterCondition("Name", QueryComparisons.NotEqual, name));
            var cloudTable = this.GetTable();
            var entitiesTable = cloudTable.ExecuteQuery(query).ToList();
            return entitiesTable;
        }

        private CloudTable GetTable()
        {
            var storageKey = this._tableManage.StorageKey;
            var tableName = StorageInfo.TableName;
            var storageAccount = CloudStorageAccount.Parse(storageKey);
            var cloudTableClient = storageAccount.CreateCloudTableClient();
            var table = cloudTableClient.GetTableReference(tableName);
            table.CreateIfNotExists();
            return table;
        }
    }
}