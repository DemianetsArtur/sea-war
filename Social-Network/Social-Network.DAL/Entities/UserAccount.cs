using Microsoft.Azure.Cosmos.Table;

namespace Social_Network.DAL.Entities
{
    public class UserAccount : TableEntity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string UserType { get; set; }
        
        public string FirstName { get;set; }
        
        public string LastName { get;set; }
        
        public string AboutMe { get; set; }
        
        public string Date { get; set; }
    }
}
