using System.ComponentModel.DataAnnotations;

namespace Bank_MVC_Project.Models
{
    public class BankTransaction
    {
        public int Id { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "The transaction amount must be larger than 0.")]
        public int Amount { get; set; }
        public DateTime Date { get; set; }

        // Used when displaying if the transaction is sent or received
        public string? SenderId { get; set; }
        [Required(ErrorMessage = "A transaction receiver is needed.")]
        public string? RecId { get; set; }

        // Many to many, both the sender and receiver
        public List<ApplicationUser> ApplicationUsers { get; set; } = new List<ApplicationUser>();
    }
}
