using Microsoft.Azure.Cosmos.Table;

namespace Social_Network.DAL.Entities
{
    public class Notification : TableEntity
    {
        public string NameResponse { get; set; }
        public string NameToResponse { get; set; }
    }
}