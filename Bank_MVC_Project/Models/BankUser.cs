namespace Bank_MVC_Project.Models
{
    public class BankUser
    {
        public int Id { get; set; }
        // Id from registration, in the AspNetUsers table.
        // Used for the program to connect AspNetUsers and BankUsers
        public string? AspNetId { get; set; }

        // BankMessage should be deletable from each user, one to many
        public List<BankMessage> Messages { get; set; } = new List<BankMessage>();

        // BankTransaction not deletable, visible for both affected users. Many to many 
        public List<BankTransaction> Transactions { get; set; } = new List<BankTransaction>();
    }
}
