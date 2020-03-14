using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HuskySite.Models;
using HuskySite.Models.BookkeepingModels;

namespace HuskySite.Controllers
{
    public class BookkeepingPatternSMSController : Controller
    {
        private readonly HuskySiteContext _context;

        public BookkeepingPatternSMSController(HuskySiteContext context)
        {
            _context = context;
        }

        // GET: BookkeepingPatternSMS
        public async Task<IActionResult> Index()
        {
            return View(await _context.PatternsSMS.ToListAsync());
        }

        // GET: BookkeepingPatternSMS/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patternSMS = await _context.PatternsSMS
                .SingleOrDefaultAsync(m => m.ID == id);
            if (patternSMS == null)
            {
                return NotFound();
            }

            return View(patternSMS);
        }

        // GET: BookkeepingPatternSMS/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookkeepingPatternSMS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Value")] PatternSMS patternSMS)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patternSMS);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patternSMS);
        }

        // GET: BookkeepingPatternSMS/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patternSMS = await _context.PatternsSMS.SingleOrDefaultAsync(m => m.ID == id);
            if (patternSMS == null)
            {
                return NotFound();
            }
            return View(patternSMS);
        }

        // POST: BookkeepingPatternSMS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Name,Value")] PatternSMS patternSMS)
        {
            if (id != patternSMS.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patternSMS);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatternSMSExists(patternSMS.ID))
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
            return View(patternSMS);
        }

        // GET: BookkeepingPatternSMS/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patternSMS = await _context.PatternsSMS
                .SingleOrDefaultAsync(m => m.ID == id);
            if (patternSMS == null)
            {
                return NotFound();
            }

            return View(patternSMS);
        }

        // POST: BookkeepingPatternSMS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var patternSMS = await _context.PatternsSMS.SingleOrDefaultAsync(m => m.ID == id);
            _context.PatternsSMS.Remove(patternSMS);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatternSMSExists(Guid id)
        {
            return _context.PatternsSMS.Any(e => e.ID == id);
        }
    }
}
