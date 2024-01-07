using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using RunStats.Data;
using RunStats.Models;
using RunStats.Services;
using RunStats.Utils;

namespace RunStats.Controllers
{
    [Authorize]
    public class RunningSessionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RunningSessionsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: RunningSessions
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            
            var allSessions = _context.RunningSession.Include(r => r.ExerciseType).Include(r => r.Shoes).Include(r => r.User).Include(r => r.Weather);
            var currentUserSessions = allSessions.Where(e => e.UserId == user.Id);
            return View(await currentUserSessions.ToListAsync());
        }

        // GET: RunningSessions/Stats
        public async Task<IActionResult> Stats()
        {
            ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
            {
                return NotFound();
            }

            // Pobierz wszystkie biegi użytkownika
            var runningSessions = await _context.RunningSession
                .Include(r => r.ExerciseType)
                .Include(r => r.Shoes)
                .Include(r => r.Weather)
                .Include(r => r.User)
                .Where(r => r.UserId == user.Id)
                .ToListAsync();

            if (runningSessions == null || runningSessions.Count == 0)
            {
                ViewData["NoDataMessage"] = "Brak danych do wyświetlenia.";
                return View();
            }
            var longestRun = runningSessions.OrderByDescending(r => r.Distance).FirstOrDefault();
            var bestPaceSession = runningSessions
                .Where(r => r.Time.HasValue)
                .OrderBy(r => r.Time / r.Distance)
                .FirstOrDefault();
            var topSessionUpTo2Km = runningSessions.Where(r => r.Distance <= 2000 && r.Time != null).OrderBy(r => r.Time / r.Distance).FirstOrDefault();
            var topSessionUpTo5Km = runningSessions.Where(r => r.Distance > 2000 && r.Distance <= 5000).OrderBy(r => r.Time / r.Distance).FirstOrDefault();
            var topSessionUpTo10Km = runningSessions.Where(r => r.Distance > 5000 && r.Distance <= 10000).OrderBy(r => r.Time / r.Distance).FirstOrDefault();
            var topSessionUpTo20Km = runningSessions.Where(r => r.Distance > 10000 && r.Distance <= 20000).OrderBy(r => r.Time / r.Distance).FirstOrDefault();
            var topSessionAbove20Km = runningSessions.Where(r => r.Distance > 20000).OrderBy(r => r.Time / r.Distance).FirstOrDefault();


            if(bestPaceSession != null)
            {
                bestPaceSession.Pace = SessionPace.CalculatePace(bestPaceSession);
            }
            if (topSessionUpTo2Km != null)
            {
                topSessionUpTo2Km.Pace = SessionPace.CalculatePace(topSessionUpTo2Km);
            }
            if (topSessionUpTo5Km != null)
            {
                topSessionUpTo5Km.Pace = SessionPace.CalculatePace(topSessionUpTo5Km);
            }
            if (topSessionUpTo10Km != null)
            {
                topSessionUpTo10Km.Pace = SessionPace.CalculatePace(topSessionUpTo10Km);
            }
            if (topSessionUpTo20Km != null)
            {
                topSessionUpTo20Km.Pace = SessionPace.CalculatePace(topSessionUpTo20Km);
            }
            if (topSessionAbove20Km != null)
            {
                topSessionAbove20Km.Pace = SessionPace.CalculatePace(topSessionAbove20Km);
            }

            ViewData["LongestRun"] = longestRun;
            ViewData["BestPace"] = bestPaceSession;
            ViewData["TopSessionUpTo2Km"] = topSessionUpTo2Km;
            ViewData["TopSessionUpTo5Km"] = topSessionUpTo5Km;
            ViewData["TopSessionUpTo10Km"] = topSessionUpTo10Km;
            ViewData["TopSessionUpTo20Km"] = topSessionUpTo20Km;
            ViewData["TopSessionAbove20Km"] = topSessionAbove20Km;

            return View();
        }

        // GET: RunningSessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (id == null || _context.RunningSession == null)
            {
                return NotFound();
            }

            var runningSession = await _context.RunningSession
                .Include(r => r.ExerciseType)
                .Include(r => r.Shoes)
                .Include(r => r.User)
                .Where(r => r.UserId == user.Id)
                .Include(r => r.Weather)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (runningSession == null)
            {
                return NotFound();
            }

            return View(runningSession);
        }

        // GET: RunningSessions/Create
        public async Task<IActionResult> Create()
        {
            ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            ViewData["ExerciseTypeId"] = new SelectList(_context.Set<ExerciseType>().Where(e => e.UserId == user.Id), "Id", "ExerciseName", "ExerciseName");
            ViewData["ShoesId"] = new SelectList(_context.Set<Shoes>().Where(s => s.UserId == user.Id), "Id", "Model", "Model");
            ViewData["UserId"] = user.Id;
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

                Shoes sessionShoes = await _context.Shoes.FindAsync(runningSession.ShoesId);

                if (sessionShoes != null)
                {
                    sessionShoes.TotalDistance += runningSession.Distance;
                    await _context.SaveChangesAsync();
                }

                 _context.Add(currentWeather);
                await _context.SaveChangesAsync();
                runningSession.WeatherId = currentWeather.Id;

                _context.Add(runningSession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(runningSession);
        }

        // GET: RunningSessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (id == null || _context.RunningSession == null)
            {
                return NotFound();
            }

            var runningSession = await _context.RunningSession.FindAsync(id);
            var shoesModel = await _context.Shoes.FindAsync(runningSession.ShoesId);
            var exerciseType = await _context.ExerciseType.FindAsync(runningSession.ExerciseTypeId);

            if (runningSession == null || runningSession.UserId != user.Id)
            {
                return NotFound();
            }
            // przed zakończeniem sesji możliwe jest usunięcie typu ćwiczenia oraz modelu obuwia - wtedy do widoku ładujemy null
            ViewData["ExerciseTypeId"] = exerciseType != null ? exerciseType.Id : null;
            ViewData["ShoesId"] = shoesModel != null ? shoesModel.Id : null;
            ViewData["ExerciseName"] = exerciseType != null ? exerciseType.ExerciseName : null;
            ViewData["ShoesModel"] = shoesModel != null ? shoesModel.Model : null;
            ViewData["UserId"] = user.Id;
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
