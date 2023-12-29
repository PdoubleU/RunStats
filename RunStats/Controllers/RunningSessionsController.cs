using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RunStats.Data;
using RunStats.Models;
using RunStats.Services;

namespace RunStats.Controllers
{
    public class RunningSessionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RunningSessionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RunningSessions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RunningSession.Include(r => r.ExerciseType).Include(r => r.Shoes).Include(r => r.User).Include(r => r.Weather);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RunningSessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RunningSession == null)
            {
                return NotFound();
            }

            var runningSession = await _context.RunningSession
                .Include(r => r.ExerciseType)
                .Include(r => r.Shoes)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (runningSession == null)
            {
                return NotFound();
            }

            return View(runningSession);
        }

        // GET: RunningSessions/Create
        public IActionResult Create()
        {
            ViewData["ExerciseTypeId"] = new SelectList(_context.Set<ExerciseType>(), "Id", "Id");
            ViewData["ShoesId"] = new SelectList(_context.Set<Shoes>(), "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: RunningSessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Distance,Time,UserId,ExerciseTypeId,ShoesId,")] RunningSession runningSession, string longitude, string latitude)
        {
            // create running session with the Weather ID
            if (ModelState.IsValid)
            {
                // load weather data from remote API and store it in DB
                WeatherService weatherService = new WeatherService();

                Weather? currentWeather = await weatherService.GetWeatherAsync(latitude, longitude, runningSession.Date);

                 _context.Add(currentWeather);
                await _context.SaveChangesAsync();
                runningSession.WeatherId = currentWeather.Id;

                _context.Add(runningSession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExerciseTypeId"] = new SelectList(_context.Set<ExerciseType>(), "Id", "Id", runningSession.ExerciseTypeId);
            ViewData["ShoesId"] = new SelectList(_context.Set<Shoes>(), "Id", "Id", runningSession.ShoesId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", runningSession.UserId);
            return View(runningSession);
        }

        // GET: RunningSessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RunningSession == null)
            {
                return NotFound();
            }

            var runningSession = await _context.RunningSession.FindAsync(id);
            if (runningSession == null)
            {
                return NotFound();
            }
            ViewData["ExerciseTypeId"] = new SelectList(_context.Set<ExerciseType>(), "Id", "Id", runningSession.ExerciseTypeId);
            ViewData["ShoesId"] = new SelectList(_context.Set<Shoes>(), "Id", "Id", runningSession.ShoesId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", runningSession.UserId);
            return View(runningSession);
        }

        // POST: RunningSessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Distance,Time,UserId,ExerciseTypeId,WeatherId,ShoesId")] RunningSession runningSession)
        {
            if (id != runningSession.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(runningSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RunningSessionExists(runningSession.Id))
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
            ViewData["ExerciseTypeId"] = new SelectList(_context.Set<ExerciseType>(), "Id", "Id", runningSession.ExerciseTypeId);
            ViewData["ShoesId"] = new SelectList(_context.Set<Shoes>(), "Id", "Id", runningSession.ShoesId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", runningSession.UserId);
            return View(runningSession);
        }

        // GET: RunningSessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RunningSession == null)
            {
                return NotFound();
            }

            var runningSession = await _context.RunningSession
                .Include(r => r.ExerciseType)
                .Include(r => r.Shoes)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (runningSession == null)
            {
                return NotFound();
            }

            return View(runningSession);
        }

        // POST: RunningSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RunningSession == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RunningSession'  is null.");
            }
            var runningSession = await _context.RunningSession.FindAsync(id);
            if (runningSession != null)
            {
                _context.RunningSession.Remove(runningSession);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RunningSessionExists(int id)
        {
            return (_context.RunningSession?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
