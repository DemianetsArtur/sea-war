namespace Social_Network.BLL.ModelsDto
{
    public class UserAccountDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string UserType { get; set; }
        
        public string FirstName { get;set; }
        
        public string LastName { get;set; }
        
        public string AboutMe { get; set; }

        public string Date { get; set; }
        
        public string ImagePath { get; set; }

        public bool EmailConfirmed { get; set; }

        public string RowKey { get; set; }

        public string PartitionKey { get; set; }

        public string EmailKey { get; set; }

        public string EmailDateKey { get; set; }
    }
}
