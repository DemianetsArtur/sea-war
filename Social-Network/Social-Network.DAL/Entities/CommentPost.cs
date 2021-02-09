using Microsoft.Azure.Cosmos.Table;

namespace Social_Network.DAL.Entities
{
    public class CommentPost : TableEntity
    {
        public string UserName { get; set; }
        public string UserImage { get; set; }
        public string UserNameResponse { get; set; }
        public string Text { get; set; }
        public string ContentName { get; set; }
    }
}
