using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Bank_MVC_Project.Models;

namespace Bank_MVC_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<BankMessage> BankMessage { get; set; }
        public DbSet<BankTransaction> BankTransaction { get; set; }
    }
}