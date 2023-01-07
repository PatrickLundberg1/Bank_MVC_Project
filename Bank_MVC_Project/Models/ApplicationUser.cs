using Microsoft.AspNetCore.Identity;

namespace Bank_MVC_Project.Models
{
    public class ApplicationUser : IdentityUser 
    {
        // Bank users money amount in Swedish Kr, starts at 1000 (test value) 
        public int Money { get; set; } = 1000;
        // BankMessage should be deletable from each user, one to many
        public List<BankMessage> Messages { get; set; } = new List<BankMessage>();

        // BankTransaction not deletable, visible for both affected users. Many to many 
        public List<BankTransaction> Transactions { get; set; } = new List<BankTransaction>();
    }
}
