using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RunStats.Data;
using RunStats.Models;

namespace RunStats.Controllers
{
    public class ShoesTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoesTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ShoesTypes
        public async Task<IActionResult> Index()
        {
              return _context.ShoesType != null ? 
                          View(await _context.ShoesType.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ShoesType'  is null.");
        }

        // GET: ShoesTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ShoesType == null)
            {
                return NotFound();
            }

            var shoesType = await _context.ShoesType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoesType == null)
            {
                return NotFound();
            }

            return View(shoesType);
        }

        // GET: ShoesTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShoesTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeName")] ShoesType shoesType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoesType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shoesType);
        }

        // GET: ShoesTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ShoesType == null)
            {
                return NotFound();
            }

            var shoesType = await _context.ShoesType.FindAsync(id);
            if (shoesType == null)
            {
                return NotFound();
            }
            return View(shoesType);
        }

        // POST: ShoesTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeName")] ShoesType shoesType)
        {
            if (id != shoesType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoesType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoesTypeExists(shoesType.Id))
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
            return View(shoesType);
        }

        // GET: ShoesTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ShoesType == null)
            {
                return NotFound();
            }

            var shoesType = await _context.ShoesType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoesType == null)
            {
                return NotFound();
            }

            return View(shoesType);
        }

        // POST: ShoesTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ShoesType == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ShoesType'  is null.");
            }
            var shoesType = await _context.ShoesType.FindAsync(id);
            if (shoesType != null)
            {
                _context.ShoesType.Remove(shoesType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoesTypeExists(int id)
        {
          return (_context.ShoesType?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
