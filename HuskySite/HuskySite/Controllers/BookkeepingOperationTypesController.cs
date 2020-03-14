using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HuskySite.Models;
using HuskySite.Models.BookkeepingModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace HuskySite.Controllers
{
    public class BookkeepingOperationTypesController : Controller
    {
        private readonly HuskySiteContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public BookkeepingOperationTypesController(HuskySiteContext context, IHostingEnvironment environment)
        {
            _context = context;
            _hostingEnvironment = environment;
        }

        // GET: BookkeepingOperationTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.OperationType.ToListAsync());
        }

        // GET: BookkeepingOperationTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operationType = await _context.OperationType
                .SingleOrDefaultAsync(m => m.ID == id);
            if (operationType == null)
            {
                return NotFound();
            }

            return View(operationType);
        }

        // GET: BookkeepingOperationTypes/Create
        public IActionResult Create()
        {
            ImageList();
            return View();
        }

        // POST: BookkeepingOperationTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Color,Icon")] OperationType operationType)
        {
            if (ModelState.IsValid)
            {
                operationType.ID = Guid.NewGuid();
                _context.Add(operationType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            


            return View(operationType);
        }

        // GET: BookkeepingOperationTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operationType = await _context.OperationType.SingleOrDefaultAsync(m => m.ID == id);
            if (operationType == null)
            {
                return NotFound();
            }
            ImageList();
            return View(operationType);
        }

        // POST: BookkeepingOperationTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Name,Color,Icon")] OperationType operationType)
        {
            if (id != operationType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(operationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperationTypeExists(operationType.ID))
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
            return View(operationType);
        }

        // GET: BookkeepingOperationTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operationType = await _context.OperationType
                .SingleOrDefaultAsync(m => m.ID == id);
            if (operationType == null)
            {
                return NotFound();
            }

            return View(operationType);
        }

        // POST: BookkeepingOperationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var operationType = await _context.OperationType.SingleOrDefaultAsync(m => m.ID == id);
            _context.OperationType.Remove(operationType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OperationTypeExists(Guid id)
        {
            return _context.OperationType.Any(e => e.ID == id);
        }

        private void ImageList()
        {
            List<KeyValuePair<string, string>> images = new List<KeyValuePair<string, string>>();
            string webRootPath = _hostingEnvironment.WebRootPath + "/images/AccTypeImg/";
            foreach (string lfile in Directory.GetFiles(webRootPath))
            {
                images.Add(new KeyValuePair<string, string>(Path.GetFileNameWithoutExtension(lfile).Replace("ic_ms_", ""), "images/AccTypeImg/" + Path.GetFileName(lfile)));
            }
            ViewBag.ImageData = images;
        }
    }
}
