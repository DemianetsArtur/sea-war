using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Azure.Cosmos.Table;
using Social_Network.DAL.Entities;
using Social_Network.DAL.Infrastructure.Interfaces;
using Social_Network.DAL.Infrastructure.Models;
using Social_Network.DAL.Manages.Tables;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Network.DAL.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly TableManage _tableManage;
        private readonly BlobServiceClient _blobServiceClient;

        public PostRepository(TableManage tableManage, 
                              BlobServiceClient blobServiceClient)
        {
            this._tableManage = tableManage;
            this._blobServiceClient = blobServiceClient;
        }

        public async Task<Uri> FileUploadToBlobAsync(Stream content, string contentType, string fileName)
        {
            var containerClient = this.GetContainerClient();
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(content, new BlobHttpHeaders { ContentType = contentType });
            return blobClient.Uri;
        }

        public void PostCreate(Post entity) 
        {
            var cloudTable = TableResponse.GetTable(this._tableManage.StorageKey, StorageInfo.PostTable);
            var operation = TableOperation.InsertOrReplace(entity);
            cloudTable.Execute(operation);
        }

        public ICollection<Post> PostsGet(string name) 
        {
            //DateTime parsedDate;
            //var dateFormat = DateTimeFormatInfo.InvariantInfo;
            //var dateNow = DateTime.Now.ToString("MM/dd/yyyy", dateFormat);
            //var tokenDate = DateTime.TryParseExact(date, OptionsInfo.DateConfig, null, DateTimeStyles.None, out parsedDate);
            var query = new TableQuery<Post>()
                .Where(TableQuery.GenerateFilterCondition(TableQueries.NameQuery, QueryComparisons.Equal, name));
            var cloudTable = TableResponse.GetTable(this._tableManage.StorageKey, StorageInfo.PostTable);
            var entitiesTable = cloudTable.ExecuteQuery(query).OrderByDescending(ord => DateTime.Parse(ord.PartitionKey)).ToList();
            return entitiesTable;
        }

        private BlobContainerClient GetContainerClient()
        {
            var containerClient = this._blobServiceClient.GetBlobContainerClient(StorageInfo.PostContainer);
            containerClient.CreateIfNotExists(PublicAccessType.None);
            return containerClient;
        }
    }
}
