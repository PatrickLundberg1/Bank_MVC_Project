namespace Bank_MVC_Project.Models
{
    public class BankTransaction
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }

        // Used when displaying if the transaction is sent or received
        public string? SenderId { get; set; }
        public string? RecId { get; set; }

        // Many to many, both the sender and receiver
        public List<ApplicationUser> ApplicationUsers { get; set; } = new List<ApplicationUser>();
    }
}
