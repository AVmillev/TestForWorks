using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HuskySite.Models;
using HuskySite.Models.BookkeepingModels;
using HuskySite.Helpers;

namespace HuskySite.Controllers
{
    public class BookkeepingAccountsController : Controller
    {
        private readonly HuskySiteContext _context;

        public BookkeepingAccountsController(HuskySiteContext context)
        {
            _context = context;
        }

        // GET: BookkeepingAccounts
        public async Task<IActionResult> Index(string sortOrder,
            string currentFilter,
            string searchString,
            int? page)
        {
            ViewData["CurrentSort"] = sortOrder;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var huskySiteContext = _context.Accounts.AsNoTracking().Include(a => a.OperationType).OrderBy(a => a.Name);

            int pageSize = 20;

            return View(await PaginatedList<Account>.CreateAsync(huskySiteContext, page ?? 1, pageSize));
        }

        // GET: BookkeepingAccounts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .SingleOrDefaultAsync(m => m.ID == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: BookkeepingAccounts/Create
        public IActionResult Create()
        {
            ViewData["OperationTypeID"] = new SelectList(_context.OperationType, "ID", "Name");
            return View();
        }

        // POST: BookkeepingAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,INN,Name,Address,OperationTypeID")] Account account)
        {
            //account.OperationType = _context.OperationType.SingleOrDefault(o => o.ID == account.OperationTypeID);
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OperationTypeID"] = new SelectList(_context.OperationType, "ID", "Name",account.OperationTypeID);
            return View(account);
        }

        // GET: BookkeepingAccounts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.AsNoTracking().SingleOrDefaultAsync(m => m.ID == id);
            if (account == null)
            {
                return NotFound();
            }
            ViewData["OperationTypeID"] = new SelectList(_context.OperationType, "ID", "Name",account.OperationTypeID);
            return View(account);
        }

        // POST: BookkeepingAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,INN,Name,Address,OperationTypeID")] Account account)
        {
           // Account preAcc = _context.Accounts.Where(a => a.ID == id).First();
            
            if (id != account.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                  
                    IEnumerable<Accounting> accountings = _context.Accountings.Where(a => a.AccountId == id);
                    foreach (Accounting accounting in accountings)
                    {
                        if (accounting.OperationTypeID != account.OperationTypeID)
                        {
                            accounting.OperationTypeID = account.OperationTypeID;
                            _context.Update(accounting);
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.ID))
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
            ViewData["OperationTypeID"] = new SelectList(_context.OperationType, "ID", "Name", account.OperationTypeID);
            return View(account);
        }

        // GET: BookkeepingAccounts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .SingleOrDefaultAsync(m => m.ID == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: BookkeepingAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var account = await _context.Accounts.SingleOrDefaultAsync(m => m.ID == id);
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(Guid id)
        {
            return _context.Accounts.Any(e => e.ID == id);
        }
    }
}
