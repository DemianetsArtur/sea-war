using Microsoft.Azure.Cosmos.Table;

namespace Social_Network.DAL.Entities
{
    public class Post : TableEntity
    {
        public string Name { get; set; }
        public string PostText { get; set; }
        public string NameContent { get; set; }
    }
}
