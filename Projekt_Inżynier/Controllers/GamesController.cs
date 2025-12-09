using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekcik.Entities;
using Projekcik.Infrastructure.Persistance;
using Projekcik.Migrations;
using Projekcik.Models; // <-- bardzo ważne


namespace Projekcik.Controllers
{
    public class GamesController : Controller
    {
        private readonly AplicationDbContext _context;

        public GamesController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            return RedirectToAction("Bracket");

     //       return View(await _context.Gry.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> GenerateFirstRound()
        {
            var robots = await _context.Roboty.ToListAsync();
            if (robots.Count < 2)
            {
                TempData["Error"] = "Za mało drużyn do utworzenia drabinki!";
                return RedirectToAction(nameof(Index));
            }
            var rnd = new Random();
            var matches = new List<Games>();

            for (int i = 0; i < robots.Count; i += 2)
            {
                if (i + 1 >= robots.Count) break; // dziala tylko dla parzystych
                                                  // wiec w ogólnym rozrachunku będzie działać tyko dla potęgi 2 robotów 

                matches.Add(new Games
                {
                    Robot1ID = robots[i].IdRobota,
                    Robot2ID = robots[i + 1].IdRobota,
                    StopienDrabinki = 1
                });
            }

            _context.Gry.AddRange(matches);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Bracket()
        {
            var matches = await _context.Gry
                .Include(m => m.Robot1)
                .Include(m => m.Robot2)
                .OrderBy(m => m.StopienDrabinki)
                .ToListAsync();

            var vm = new BracketViewModel
            {
                Matches = matches,
                CurrentRound = matches.Any() ? matches.Min(m => m.StopienDrabinki) : 1
            };

            return View(vm);
        }

    }
}