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
    public class ExcerciseTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExcerciseTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ExcerciseTypes
        public async Task<IActionResult> Index()
        {
              return _context.ExcerciseType != null ? 
                          View(await _context.ExcerciseType.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ExcerciseType'  is null.");
        }

        // GET: ExcerciseTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ExcerciseType == null)
            {
                return NotFound();
            }

            var excerciseType = await _context.ExcerciseType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (excerciseType == null)
            {
                return NotFound();
            }

            return View(excerciseType);
        }

        // GET: ExcerciseTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExcerciseTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExcerciseName")] ExcerciseType excerciseType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(excerciseType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(excerciseType);
        }

        // GET: ExcerciseTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ExcerciseType == null)
            {
                return NotFound();
            }

            var excerciseType = await _context.ExcerciseType.FindAsync(id);
            if (excerciseType == null)
            {
                return NotFound();
            }
            return View(excerciseType);
        }

        // POST: ExcerciseTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExcerciseName")] ExcerciseType excerciseType)
        {
            if (id != excerciseType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(excerciseType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExcerciseTypeExists(excerciseType.Id))
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
            return View(excerciseType);
        }

        // GET: ExcerciseTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ExcerciseType == null)
            {
                return NotFound();
            }

            var excerciseType = await _context.ExcerciseType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (excerciseType == null)
            {
                return NotFound();
            }

            return View(excerciseType);
        }

        // POST: ExcerciseTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ExcerciseType == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ExcerciseType'  is null.");
            }
            var excerciseType = await _context.ExcerciseType.FindAsync(id);
            if (excerciseType != null)
            {
                _context.ExcerciseType.Remove(excerciseType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExcerciseTypeExists(int id)
        {
          return (_context.ExcerciseType?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
