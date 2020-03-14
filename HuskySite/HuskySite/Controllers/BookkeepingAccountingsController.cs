using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HuskySite.Models;
using HuskySite.Models.BookkeepingModels;
using HuskySite.Services;
using Newtonsoft.Json.Linq;
using HuskySite.Helpers;
using Microsoft.AspNetCore.Identity;

namespace HuskySite.Controllers
{
    public class BookkeepingAccountingsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HuskySiteContext _context;

        public BookkeepingAccountingsController(HuskySiteContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: BookkeepingAccountings
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

            var huskySiteContext = _context.Accountings.Include(a => a.Account).Include(a => a.Wallet)
                .Include(a => a.OperationType).Include(a => a.Account.OperationType).OrderByDescending(a => a.DateTimeOperation);

            int pageSize = 20;

            //return View(await huskySiteContext.ToListAsync());
            return View(await PaginatedList<Accounting>.CreateAsync(huskySiteContext, page ?? 1, pageSize));
        }

        // GET: BookkeepingAccountings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accounting = await _context.Accountings
                .Include(a => a.Account)
                .Include(a => a.Wallet)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (accounting == null)
            {
                return NotFound();
            }

            return View(accounting);
        }

        public IActionResult AddReciept()
        {
            ViewData["WalletID"] = new SelectList(_context.Wallets.Where(w => w.UserID == _userManager.GetUserId(User)), "ID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReciept([Bind("ID,WalletID,FN,FD,FP")] Accounting accounting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accounting);


                if (accounting.FD != null && accounting.FN != null && accounting.FP != null)
                {
                    NalogData data = new NalogData();
                    string json = await data.GetReceiptAsinch(accounting.FD, accounting.FN, accounting.FP);
                    accounting.RecieptJSON = json;
                    Receipt rec = ReceiptToClass.FromJson(json).Document.Receipt;
                    IQueryable<Account> accQuery = (from a in _context.Accounts
                                                    where a.INN == Convert.ToInt64(rec.UserInn)
                                                    select a);
                    if (accQuery.Count() > 0)
                    {
                        accounting.Account = accQuery.First();
                        accounting.OperationTypeID = accQuery.First().OperationTypeID;
                    }
                    else
                    {
                        Account lAcc = new Account()
                        {
                            ID = new Guid(),
                            INN = Convert.ToInt64(rec.UserInn),
                            Name = rec.User,
                            Address = rec.RetailPlaceAddress,
                            OperationTypeID = _context.OperationType.Where(o => o.Name == "default").Select(o => o.ID).First()
                        };
                        _context.Update(lAcc);
                        accounting.Account = lAcc;
                        accounting.OperationTypeID = _context.OperationType.Where(o => o.Name == "default").Select(o => o.ID).First();
                    }

                    accounting.Summ = rec.TotalSum / 100;
                    accounting.DateTimeOperation = rec.DateTime.DateTime;
                    accounting.AccountingType = AccountingTypes.Expense;
                    accounting.Currency = Currencyes.rub;
                }

                if (accounting.Summ != null && accounting.Summ.Value > 0)
                {
                    decimal sum = accounting.Summ.Value;
                    Wallet wallet = (from w in _context.Wallets
                                     where w.ID == accounting.WalletID
                                     && w.UserID == _userManager.GetUserId(User)
                                     select w).First();
                    if (accounting.AccountingType == 0)
                        wallet.Balance = wallet.Balance + sum;
                    else wallet.Balance = wallet.Balance - sum;
                    _context.Update(wallet);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }     

            return View(accounting);
        }

        // GET: BookkeepingAccountings/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Accounts, "ID", "Name");
            ViewData["OperationTypeID"] = new SelectList(_context.OperationType, "ID", "Name");
            ViewData["WalletID"] = new SelectList(_context.Wallets.Where(w => w.UserID == _userManager.GetUserId(User)), "ID", "Name");
            return View();
        }

        // POST: BookkeepingAccountings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,WalletID,Currency,DateTimeOperation,Description,Summ,FN,FD,FP,AccountId,AccountingType,OperationTypeID")] Accounting accounting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accounting);          

                if (accounting.Summ != null && accounting.Summ.Value > 0)
                {
                    decimal sum = accounting.Summ.Value;
                    Wallet wallet = (from w in _context.Wallets
                                     where w.ID == accounting.WalletID
                                     && w.UserID == _userManager.GetUserId(User)
                                     select w).First();
                    if (accounting.AccountingType == 0)
                        wallet.Balance = wallet.Balance + sum;
                    else wallet.Balance = wallet.Balance - sum;
                    _context.Update(wallet);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "ID", "Name", accounting.AccountId);
            ViewData["WalletID"] = new SelectList(_context.Wallets, "ID", "Name", accounting.WalletID);
            ViewData["OperationTypeID"] = new SelectList(_context.OperationType, "ID", "Name",accounting.OperationTypeID);



            return View(accounting);
        }

        // GET: BookkeepingAccountings/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accounting = await _context.Accountings.SingleOrDefaultAsync(m => m.ID == id);
            //accounting.OperationType = accounting.Account.OperationType;
            if (accounting == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "ID", "Name", accounting.AccountId);
            ViewData["WalletID"] = new SelectList(_context.Wallets.Where(w => w.UserID == _userManager.GetUserId(User)), "ID", "Name", accounting.WalletID);
            ViewData["OperationTypeID"] = new SelectList(_context.OperationType, "ID", "Name", accounting.OperationTypeID);
            return View(accounting);
        }

        // POST: BookkeepingAccountings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,WalletID,Currency,DateTimeOperation,Description,Summ,FN,FD,FP,AccountId,AccountingType,OperationTypeID")] Accounting accounting)
        {
            if (id != accounting.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accounting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountingExists(accounting.ID))
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
            ViewData["AccountId"] = new SelectList(_context.Accounts, "ID", "ID", accounting.AccountId);
            ViewData["WalletID"] = new SelectList(_context.Wallets, "ID", "ID", accounting.WalletID);
            ViewData["OperationTypeID"] = new SelectList(_context.OperationType, "ID", "Name", accounting.OperationTypeID);
            return View(accounting);
        }

        // GET: BookkeepingAccountings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accounting = await _context.Accountings
                .Include(a => a.Account)
                .Include(a => a.Wallet)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (accounting == null)
            {
                return NotFound();
            }

            return View(accounting);
        }

        // POST: BookkeepingAccountings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var accounting = await _context.Accountings.SingleOrDefaultAsync(m => m.ID == id);
            if (accounting.Summ != null && accounting.Summ.Value > 0)
            {
                decimal sum = accounting.Summ.Value;
                Wallet wallet = (from w in _context.Wallets
                                 where w.ID == accounting.WalletID
                                 select w).First();
                if (accounting.AccountingType == 0)
                    wallet.Balance = wallet.Balance - sum;
                else wallet.Balance = wallet.Balance + sum;
                _context.Update(wallet);
            }

            _context.Accountings.Remove(accounting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountingExists(Guid id)
        {
            return _context.Accountings.Any(e => e.ID == id);
        }
    }
}
