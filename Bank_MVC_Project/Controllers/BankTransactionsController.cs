using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bank_MVC_Project.Data;
using Bank_MVC_Project.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Policy;

namespace Bank_MVC_Project.Controllers
{
    public class BankTransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BankTransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BankTransactions
        public async Task<IActionResult> Index()
        {
            return View(await _context.BankTransaction.Include(b => b.ApplicationUsers).ToListAsync());
        }

        // GET: BankTransactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BankTransaction == null)
            {
                return NotFound();
            }

            var bankTransaction = await _context.BankTransaction
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bankTransaction == null)
            {
                return NotFound();
            }

            return View(bankTransaction);
        }

        // GET: BankTransactions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BankTransactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,Date,SenderId,RecId")] BankTransaction bankTransaction)
        {
            if(bankTransaction.SenderId == bankTransaction.RecId)
            {
                return BadRequest("You can not send a transaction to yourself.");
            }
            else if(bankTransaction.Amount <= 0)
            {
                return BadRequest("The transaction amount needs to be larger than zero to be legitimate.");
            }

            ApplicationUser? sender = await _context.FindAsync<ApplicationUser>(bankTransaction.SenderId);
            ApplicationUser? receiver = await _context.FindAsync<ApplicationUser>(bankTransaction.RecId);

            if(sender == null || receiver == null)
            {
                return BadRequest("The transaction receiver does not exist.");
            }

            int money_left = sender.Money;
            if(money_left < bankTransaction.Amount)
            {
                //return View(bankTransaction);
                return BadRequest("The transaction amount exceeds your current amount of money.");
                //return Problem("Not enough money left on your account for the transaction.");
            }

            if (ModelState.IsValid)
            {
                sender.Money -= bankTransaction.Amount;
                sender.Transactions.Add(bankTransaction);

                receiver.Money += bankTransaction.Amount;
                receiver.Transactions.Add(bankTransaction);

                _context.Update(sender);
                _context.Update(receiver);

                _context.Add(bankTransaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bankTransaction);
        }

        private bool BankTransactionExists(int id)
        {
            return _context.BankTransaction.Any(e => e.Id == id);
        }
    }
}
