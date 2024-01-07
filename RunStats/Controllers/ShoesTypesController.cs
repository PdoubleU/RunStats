using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RunStats.Data;
using RunStats.Models;

namespace RunStats.Controllers
{
    [Authorize]
    public class ShoesTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoesTypesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ShoesTypes
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            return _context.ShoesType != null ?
                        View(await _context.ShoesType.Where(s => s.UserId == user.Id).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.ShoesType'  is null.");
        }

        // GET: ShoesTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (id == null || _context.ShoesType == null)
            {
                return NotFound();
            }

            var shoesType = await _context.ShoesType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoesType == null || shoesType.UserId != user.Id)
            {
                return NotFound();
            }

            return View(shoesType);
        }

        // GET: ShoesTypes/Create
        public async Task<IActionResult> Create()
        {
            ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            ViewData["UserId"] = user.Id;

            return View();
        }

        // POST: ShoesTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeName,UserId")] ShoesType shoesType)
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
        public async Task<IActionResult> Edit([Bind("UserId")] int? id)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (id == null || _context.ShoesType == null)
            {
                return NotFound();
            }

            var ShoesType = await _context.ShoesType.FindAsync(id);
            if (ShoesType == null || ShoesType.UserId != user.Id)
            {
                return NotFound();
            }

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ShoesType.UserId);
            return View(ShoesType);
        }

        // POST: ShoesTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeName, UserId")] ShoesType shoesType)
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
