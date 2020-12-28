using Microsoft.Azure.Cosmos.Table;

namespace Social_Network.DAL.Entities
{
    public class Friend : TableEntity
    {
        public string UserNameResponse { get; set; }
        public string UserNameToResponse { get; set; }
    }
}