using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HuskySite.Models;
using HuskySite.Models.BookkeepingModels;
using Microsoft.AspNetCore.Identity;

namespace HuskySite.Controllers
{
    public class BookkeepingWalletsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HuskySiteContext _context;

        public BookkeepingWalletsController(HuskySiteContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: BookkeepingWallets
        public async Task<IActionResult> Index()
        {
            var huskySiteContext = _context.Wallets.Where(w => w.UserID == _userManager.GetUserId(User));//.Include(w => w.PatternSMS);
            return View(await huskySiteContext.ToListAsync());
        }

        // GET: BookkeepingWallets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wallet = await _context.Wallets
                .Include(w => w.PatternSMS)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (wallet == null)
            {
                return NotFound();
            }

            return View(wallet);
        }

        // GET: BookkeepingWallets/Create
        public IActionResult Create()
        {
            PatternSMSDropDownList();
            return View();
        }

        // POST: BookkeepingWallets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,UserID,PatternSMSID,Balance,PaymentType")] Wallet wallet)
        {
            if (ModelState.IsValid)
            {
                wallet.UserID = _userManager.GetUserId(User);
                _context.Add(wallet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PatternSMSDropDownList();
            return View(wallet);
        }

        // GET: BookkeepingWallets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            PatternSMSDropDownList();
            if (id == null)
            {
                return NotFound();
            }

            var wallet = await _context.Wallets.SingleOrDefaultAsync(m => m.ID == id);
            if (wallet == null)
            {
                return NotFound();
            }
            PatternSMSDropDownList();
            return View(wallet);
        }

        // POST: BookkeepingWallets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Name,UserID,PatternSMSID,Balance,PaymentType")] Wallet wallet)
        {
            if (id != wallet.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wallet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WalletExists(wallet.ID))
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
            PatternSMSDropDownList();
            return View(wallet);
        }

        // GET: BookkeepingWallets/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wallet = await _context.Wallets
                .Include(w => w.PatternSMS)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (wallet == null)
            {
                return NotFound();
            }

            return View(wallet);
        }

        // POST: BookkeepingWallets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var wallet = await _context.Wallets.SingleOrDefaultAsync(m => m.ID == id);
            _context.Wallets.Remove(wallet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WalletExists(Guid id)
        {
            return _context.Wallets.Any(e => e.ID == id);
        }

        private void PatternSMSDropDownList(object selectedDepartment = null)
        {
            var departmentsQuery = from d in _context.PatternsSMS
                                   orderby d.Name
                                   select d;
            ViewBag.PatternSMSID = new SelectList(departmentsQuery.AsNoTracking(), "ID", "Name", selectedDepartment);
            ViewData["PatternSMSID"] = new SelectList(departmentsQuery.AsNoTracking(), "ID", "Name", selectedDepartment);
        }
    }
}
