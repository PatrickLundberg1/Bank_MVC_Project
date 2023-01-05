namespace Bank_MVC_Project.Models
{
    public class BankTransaction
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }

        // Used when displaying if the transaction is sent or received
        public int SenderId { get; set; }
        public int RecId { get; set; }

        // Many to many, both the sender and receiver
        public List<BankUser> BankUsers { get; set; } = new List<BankUser>();
    }
}
