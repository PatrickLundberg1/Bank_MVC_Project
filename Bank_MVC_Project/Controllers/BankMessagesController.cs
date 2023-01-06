using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bank_MVC_Project.Data;
using Bank_MVC_Project.Models;

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
            ViewData["BankUserId"] = new SelectList(_context.Set<BankUser>(), "Id", "Id");
            return View();
        }

        // POST: BankMessages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Message,Date,BankUserId")] BankMessage bankMessage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bankMessage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BankUserId"] = new SelectList(_context.Set<BankUser>(), "Id", "Id", bankMessage.BankUserId);
            return View(bankMessage);
        }

        // GET: BankMessages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BankMessage == null)
            {
                return NotFound();
            }

            var bankMessage = await _context.BankMessage.FindAsync(id);
            if (bankMessage == null)
            {
                return NotFound();
            }
            ViewData["BankUserId"] = new SelectList(_context.Set<BankUser>(), "Id", "Id", bankMessage.BankUserId);
            return View(bankMessage);
        }

        // POST: BankMessages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Message,Date,BankUserId")] BankMessage bankMessage)
        {
            if (id != bankMessage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bankMessage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankMessageExists(bankMessage.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BankUserId"] = new SelectList(_context.Set<BankUser>(), "Id", "Id", bankMessage.BankUserId);
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
