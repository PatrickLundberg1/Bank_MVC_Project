using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bank_MVC_Project.Data;
using Bank_MVC_Project.Models;
using NuGet.Protocol.Plugins;

namespace Bank_MVC_Project.Controllers
{
    public class BankMessagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BankMessagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BankMessages
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BankMessage.Include(b => b.MessageReceiver);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BankMessages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BankMessage == null)
            {
                return NotFound();
            }

            var bankMessage = await _context.BankMessage
                .Include(b => b.MessageReceiver)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bankMessage == null)
            {
                return NotFound();
            }

            return View(bankMessage);
        }

        // GET: BankMessages/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: BankMessages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Message,Date,ApplicationUserId")] BankMessage bankMessage)
        {
            ApplicationUser? receiver = await _context.FindAsync<ApplicationUser>(bankMessage.ApplicationUserId);

            if (receiver == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                receiver.Messages.Add(bankMessage);

                _context.Update(receiver);

                _context.Add(bankMessage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", bankMessage.ApplicationUserId);
            return View(bankMessage);
        }

        // GET: BankMessages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BankMessage == null)
            {
                return NotFound();
            }

            var bankMessage = await _context.BankMessage
                .Include(b => b.MessageReceiver)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bankMessage == null)
            {
                return NotFound();
            }

            return View(bankMessage);
        }

        // POST: BankMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BankMessage == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BankMessage'  is null.");
            }
            var bankMessage = await _context.BankMessage.FindAsync(id);
            if (bankMessage != null)
            {
                _context.BankMessage.Remove(bankMessage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankMessageExists(int id)
        {
            return _context.BankMessage.Any(e => e.Id == id);
        }
    }
}
