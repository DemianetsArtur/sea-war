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
            var cloudTable = TableResponse.GetTable(this._tableManage.StorageKey, StorageInfo.UserAccountTable);
            var operation = TableOperation.InsertOrReplace(entity);
            cloudTable.Execute(operation);
        }

        public bool UserAccountFind(UserAccount entity)
        {
            var query = new TableQuery<UserAccount>()
                .Where(TableQuery.GenerateFilterCondition(TableQueries.UserAccountNameQuery, QueryComparisons.Equal, entity.Name));
            var cloudTable = TableResponse.GetTable(this._tableManage.StorageKey, StorageInfo.UserAccountTable);
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
            var cloudTable = TableResponse.GetTable(this._tableManage.StorageKey, StorageInfo.UserAccountTable);
            return cloudTable.ExecuteQuery(query).FirstOrDefault();
        }

        public void UserAccountReplace(string name, string imagePath)
        {
            var query = new TableQuery<UserAccount>()
                .Where(TableQuery.GenerateFilterCondition(TableQueries.UserAccountNameQuery, QueryComparisons.Equal, name));
            var cloudTable = TableResponse.GetTable(this._tableManage.StorageKey, StorageInfo.UserAccountTable);
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
            var cloudTable = TableResponse.GetTable(this._tableManage.StorageKey, StorageInfo.UserAccountTable);
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

        public UserAccount EmailConfirmation(string key) 
        {
            var query = new TableQuery<UserAccount>()
                .Where(TableQuery.GenerateFilterCondition(TableQueries.EmailKeyQuery, QueryComparisons.Equal, key));
            var cloudTable = TableResponse.GetTable(this._tableManage.StorageKey, StorageInfo.UserAccountTable);
            var entitiesTable = cloudTable.ExecuteQuery(query).FirstOrDefault();
            if (entitiesTable != null)
            {
                entitiesTable.EmailConfirmed = true;
                this.UserAccountCreate(entitiesTable);
                return entitiesTable;
            }
            else 
            {
                return null;
            }
        }
    }
}
