namespace Bank_MVC_Project.Models
{
    public class BankMessage
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public DateTime Date { get; set; }

        // Link to BankUser, BankMessage is deletable for each user so each user gets a unique object
        public int BankUserId { get; set; }
        public BankUser MessageReceiver { get; set; } = new BankUser();
    }
}
