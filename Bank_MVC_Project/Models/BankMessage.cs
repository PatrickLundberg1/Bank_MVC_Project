using System.ComponentModel.DataAnnotations;

namespace Bank_MVC_Project.Models
{
    public class BankMessage
    {
        public int Id { get; set; }
        [Required()]
        public string? Message { get; set; }
        public DateTime Date { get; set; }

        // Link to BankUser, BankMessage is deletable for each user so each user gets a unique object
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? MessageReceiver { get; set; }
    }
}
