using Microsoft.Azure.Cosmos.Table;
using Social_Network.DAL.Entities;
using Social_Network.DAL.Infrastructure.Interfaces;
using Social_Network.DAL.Infrastructure.Models;
using Social_Network.DAL.Manages.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Social_Network.DAL.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly TableManage _tableManage;

        public CommentRepository(TableManage tableManage)
        {
            this._tableManage = tableManage;
        }

        public void CommentPostCreate(CommentPost entity) 
        {
            var cloudTable = TableResponse.GetTable(this._tableManage.StorageKey, StorageInfo.CommentPostTable);
            var operation = TableOperation.InsertOrReplace(entity);
            cloudTable.Execute(operation);
        }

        public ICollection<CommentPost> CommentPostGet(string name) 
        {
            var query = new TableQuery<CommentPost>()
                .Where(TableQuery.GenerateFilterCondition(TableQueries.UserNameResponseQuery, QueryComparisons.Equal, name));
            var cloudTable = TableResponse.GetTable(this._tableManage.StorageKey, StorageInfo.CommentPostTable);
            var entitiesTable = cloudTable.ExecuteQuery(query).OrderBy(ord => DateTime.Parse(ord.PartitionKey)).ToList();
            return entitiesTable;
        }

        public ICollection<CommentPost> CommentPostsGetAll() 
        {
            var cloudTable = TableResponse.GetTable(this._tableManage.StorageKey, StorageInfo.CommentPostTable);
            var query = new TableQuery<CommentPost>();
            var entitiesTable = cloudTable.ExecuteQuery(query).OrderBy(ord => DateTime.Parse(ord.PartitionKey)).ToList();
            return entitiesTable;
        }
    }
}
