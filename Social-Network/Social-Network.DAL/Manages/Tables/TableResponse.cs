using Microsoft.Azure.Cosmos.Table;

namespace Social_Network.DAL.Manages.Tables
{
    public static class TableResponse
    {
        public static CloudTable GetTable(string key, string name)
        {
            var storageKey = key;
            var tableName = name;
            var storageAccount = CloudStorageAccount.Parse(storageKey);
            var cloudTableClient = storageAccount.CreateCloudTableClient();
            var table = cloudTableClient.GetTableReference(tableName);
            table.CreateIfNotExists();
            return table;
        }
    }
}