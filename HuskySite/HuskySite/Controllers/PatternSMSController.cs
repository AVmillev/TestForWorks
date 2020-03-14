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
    public class PatternSMSController : Controller
    {
        private readonly HuskySiteContext _context;

        public PatternSMSController(HuskySiteContext context)
        {
            _context = context;
        }

        // GET: PatternSMS
        public async Task<IActionResult> PatternSMS()
        {
            return View(await _context.PatternsSMS.ToListAsync());
        }

        // GET: PatternSMS/Details/5
        public async Task<IActionResult> DetailsPatternSMS(Guid? id)
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

        // GET: PatternSMS/Create
        public IActionResult CreatePatternSMS()
        {
            return View();
        }

        // POST: PatternSMS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value")] PatternSMS patternSMS)
        {
            if (ModelState.IsValid)
            {
                patternSMS.ID = Guid.NewGuid();
                _context.Add(patternSMS);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(PatternSMS));
            }
            return View(patternSMS);
        }

        // GET: PatternSMS/Edit/5
        public async Task<IActionResult> EditPatternSMS(Guid? id)
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

        // POST: PatternSMS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPatternSMS(Guid id, [Bind("Id,Value")] PatternSMS patternSMS)
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
                return RedirectToAction(nameof(PatternSMS));
            }
            return View(patternSMS);
        }

        // GET: PatternSMS/Delete/5
        public async Task<IActionResult> DeletePatternSMS(Guid? id)
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

        // POST: PatternSMS/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePatternSMS(Guid id)
        {
            var patternSMS = await _context.PatternsSMS.SingleOrDefaultAsync(m => m.ID == id);
            _context.PatternsSMS.Remove(patternSMS);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(PatternSMS));
        }

        private bool PatternSMSExists(Guid id)
        {
            return _context.PatternsSMS.Any(e => e.ID == id);
        }
    }
}
