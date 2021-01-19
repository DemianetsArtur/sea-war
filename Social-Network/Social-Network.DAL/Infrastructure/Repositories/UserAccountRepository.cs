using System.Collections.Generic;
using Microsoft.Azure.Cosmos.Table;
using Social_Network.DAL.Entities;
using Social_Network.DAL.Infrastructure.Interfaces;
using Social_Network.DAL.Manages.Tables;
using System.Linq;
using Social_Network.DAL.Infrastructure.Models;

namespace Social_Network.DAL.Infrastructure.Repositories
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly TableManage _tableManage;

        public UserAccountRepository(TableManage tableManage)
        {
            this._tableManage = tableManage;
        }

        public void UserAccountCreate(UserAccount entity) 
        {
            var cloudTable = this.GetTable();
            var operation = TableOperation.InsertOrReplace(entity);
            cloudTable.Execute(operation);
        }

        public bool UserAccountFind(UserAccount entity)
        {
            var query = new TableQuery<UserAccount>()
                .Where(TableQuery.GenerateFilterCondition(TableQueries.UserAccountNameQuery, QueryComparisons.Equal, entity.Name));
            var cloudTable = this.GetTable();
            var entitiesTable = cloudTable.ExecuteQuery(query).FirstOrDefault();
            if (entitiesTable != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public UserAccount UserGet(string name)
        {
            var query = new TableQuery<UserAccount>()
                .Where(TableQuery.GenerateFilterCondition(TableQueries.UserAccountNameQuery, QueryComparisons.Equal, name));
            var cloudTable = this.GetTable();
            return cloudTable.ExecuteQuery(query).FirstOrDefault();
        }

        public void UserAccountReplace(string name, string imagePath)
        {
            var query = new TableQuery<UserAccount>()
                .Where(TableQuery.GenerateFilterCondition(TableQueries.UserAccountNameQuery, QueryComparisons.Equal, name));
            var cloudTable = this.GetTable();
            var entitiesTable = cloudTable.ExecuteQuery(query).FirstOrDefault();
            if (entitiesTable != null)
            {
                entitiesTable.ImagePath = imagePath;
                this.UserAccountCreate(entitiesTable);
            }
        }

        public UserAccount UserAccountLoginFind(UserAccount entity)
        {
            var nameQuery = TableQuery.GenerateFilterCondition(TableQueries.UserAccountNameQuery, QueryComparisons.Equal, entity.Name);
            var passwordQuery = TableQuery.GenerateFilterCondition(TableQueries.PasswordQuery, QueryComparisons.Equal, entity.Password);
            var combineQuery = TableQuery.CombineFilters(nameQuery, TableOperators.And, passwordQuery);

            var query = new TableQuery<UserAccount>().Where(combineQuery);
            var cloudTable = this.GetTable();
            var entitiesTable = cloudTable.ExecuteQuery(query).FirstOrDefault();
            if (entitiesTable != null)
            {
                return entitiesTable;
            }
            else
            {
                return null;
            }
        }

        public ICollection<UserAccount> UserAll()
        {
            var cloudTable = TableResponse.GetTable(this._tableManage.StorageKey, StorageInfo.UserAccountTable);
            var query = new TableQuery<UserAccount>();
            var entitiesTable = cloudTable.ExecuteQuery(query).ToList();
            return entitiesTable;
        }

        private CloudTable GetTable()
        {
            var storageKey = this._tableManage.StorageKey;
            var tableName = StorageInfo.UserAccountTable;
            var storageAccount = CloudStorageAccount.Parse(storageKey);
            var cloudTableClient = storageAccount.CreateCloudTableClient();
            var table = cloudTableClient.GetTableReference(tableName);
            table.CreateIfNotExists();
            return table;
        }
    }
}
