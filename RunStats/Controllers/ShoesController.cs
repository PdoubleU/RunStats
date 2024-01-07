using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RunStats.Data;
using RunStats.Models;

namespace RunStats.Controllers
{
    [Authorize]
    public class ShoesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Shoes
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            var applicationDbContext = _context.Shoes.Include(s => s.ShoesType).Include(s => s.User);
            return View(await applicationDbContext.Where(s => s.UserId == user.Id).ToListAsync());
        }

        // GET: Shoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (id == null || _context.Shoes == null)
            {
                return NotFound();
            }

            var shoes = await _context.Shoes
                .Include(s => s.ShoesType)
                .Include(s => s.User)
                .Where(s => s.UserId == user.Id)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoes == null)
            {
                return NotFound();
            }

            return View(shoes);
        }

        // GET: Shoes/Create
        public async Task<IActionResult> Create()
        {
            ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            var shoesList = new SelectList(_context.Set<ShoesType>().Where(s => s.UserId == user.Id), "Id", "TypeName", "TypeName");

            ViewData["UserId"] = user.Id;
            ViewData["ShoesTypeId"] = shoesList.Any() ? shoesList : null;

            return View();
        }

        // POST: Shoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Model,TotalDistance,ShoesTypeId,UserId")] Shoes shoes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shoes);
        }

        // GET: Shoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (id == null || _context.Shoes == null)
            {
                return NotFound();
            }

            var shoes = await _context.Shoes.FindAsync(id);
            if (shoes == null || shoes.UserId != user.Id)
            {
                return NotFound();
            }

            ViewData["UserId"] = user.Id;
            ViewData["ShoesTypeId"] = new SelectList(_context.Set<ShoesType>().Where(s => s.UserId == user.Id), "Id" , "TypeName", "TypeName");
            return View(shoes);
        }

        // POST: Shoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Model,TotalDistance,ShoesTypeId,UserId")] Shoes shoes)
        {
            if (id != shoes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoesExists(shoes.Id))
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
            return View(shoes);
        }

        // GET: Shoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Shoes == null)
            {
                return NotFound();
            }

            var shoes = await _context.Shoes
                .Include(s => s.ShoesType)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoes == null)
            {
                return NotFound();
            }

            return View(shoes);
        }

        // POST: Shoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Shoes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Shoes'  is null.");
            }
            var shoes = await _context.Shoes.FindAsync(id);
            var sessions = from s in _context.RunningSession
                           where s.ShoesId == shoes.Id 
                           select s;
            if (sessions.Any())
            {
                foreach (var session in sessions)
                {
                    session.ShoesId = null;

                }
            }
            await _context.SaveChangesAsync();

            if (shoes != null)
            {
                _context.Shoes.Remove(shoes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoesExists(int id)
        {
            return (_context.Shoes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
