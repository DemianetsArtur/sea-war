using System;

namespace Social_Network.DAL.Manages.Tables
{
    public class TableManage
    {
        public TableManage(string storageAccount, string storageKey)
        {
            if (string.IsNullOrEmpty(storageAccount)) { throw new ArgumentNullException("storage account is null"); }
            if (string.IsNullOrEmpty(storageKey)) { throw new ArgumentNullException("storage key is null"); }

            this.StorageAccount = storageAccount;
            this.StorageKey = storageKey;
        }

        public string StorageAccount { get; }

        public string StorageKey { get; }

    }
}
