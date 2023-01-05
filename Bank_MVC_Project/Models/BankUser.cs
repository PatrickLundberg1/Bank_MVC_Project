namespace Bank_MVC_Project.Models
{
    public class BankUser
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // BankMessage should be deletable from each user, one to many
        public List<BankMessage> Messages { get; set; } = new List<BankMessage>();

        // BankTransaction not deletable, visible for both affected users. Many to many 
        public List<BankTransaction> Transactions { get; set; } = new List<BankTransaction>();
    }
}
