using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekcik.application.Robots;
using Projekcik.application.Users;
using Projekcik.Entities;
using Projekcik.Infrastructure.Persistance;

namespace Projekcik.Controllers
{
    public class RobotsController : Controller
    {
        private readonly AplicationDbContext _context;
        private readonly IMapper _mapper;

        public RobotsController(AplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Robots
        public async Task<IActionResult> Index()
        {
            var robots = await _context.Roboty
                .Include(r => r.Team)
                .Include(r => r.Categories)
                .Include(r => r.Zawodnik)
                .ToListAsync();

            var dtos = _mapper.Map<List<RobotsDto>>(robots);

            return View(dtos);
        }

        // GET: Robots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var robot = await _context.Roboty
                .Include(r => r.Team)
                .Include(r => r.Categories)
                .Include(r => r.Zawodnik) // Dodano include, żeby widzieć operatora w szczegółach
                .FirstOrDefaultAsync(m => m.IdRobota == id);

            if (robot == null) return NotFound();

            return View(robot);
        }

        // GET: Robots/Create
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        // POST: Robots/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RobotCreateDto dto)
        {
            if (await _context.Roboty.AnyAsync(u => u.NazwaRobota == dto.NazwaRobota && u.IdKategorii == dto.IdKategorii))
            {
                ModelState.AddModelError("NazwaRobota", "Taki robot już istnieje w tej kategorii.");
            }

            // 2. TERAZ sprawdzamy IsValid (który zwróci false, jeśli powyższy if dodał błąd)
            if (ModelState.IsValid)
            {
                var robot = new Robots
                {
                    NazwaRobota = dto.NazwaRobota,
                    IdKategorii = dto.IdKategorii,
                    IdDruzyny = dto.IdDruzyny,
                    IdZawodnika = dto.IdZawodnika
                };

                _context.Add(robot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // 3. Jeśli ModelState NIE jest valid (bo duplikat albo inne błędy), wracamy do widoku
            PopulateDropdowns(dto.IdDruzyny, dto.IdKategorii, dto.IdZawodnika);
            return View(dto);
        }

        // GET: Robots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var robot = await _context.Roboty.FindAsync(id);
            if (robot == null) return NotFound();

            var dto = new RobotEditDto
            {
                IdRobota = robot.IdRobota,
                NazwaRobota = robot.NazwaRobota,
                IdKategorii = robot.IdKategorii,
                IdDruzyny = robot.IdDruzyny,
                IdZawodnika = robot.IdZawodnika
            };

            // Ładujemy listy i zaznaczamy to, co jest aktualnie w bazie
            PopulateDropdowns(robot.IdDruzyny, robot.IdKategorii, robot.IdZawodnika);

            return View(dto);
        }

        // POST: Robots/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RobotEditDto dto)
        {
            if (id != dto.IdRobota) return NotFound();

            // 1. SPRAWDZANIE UNIKALNOŚCI (Z WYKLUCZENIEM SIEBIE)
            if (await _context.Roboty.AnyAsync(u => u.NazwaRobota== dto.NazwaRobota && u.IdKategorii == dto.IdKategorii && u.IdRobota != dto.IdRobota))
            {
                ModelState.AddModelError("NazwaRobota", "Taki robot już istnieje w tej kategorii.");
            }

            // 2. WALIDACJA
            if (ModelState.IsValid)
            {
                try
                {
                    var robotToUpdate = await _context.Roboty.FindAsync(id);
                    if (robotToUpdate == null) return NotFound();

                    robotToUpdate.NazwaRobota = dto.NazwaRobota;
                    robotToUpdate.IdKategorii = dto.IdKategorii;
                    robotToUpdate.IdDruzyny = dto.IdDruzyny;
                    robotToUpdate.IdZawodnika = dto.IdZawodnika;

                    _context.Update(robotToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RobotsExists(dto.IdRobota)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            // 3. Jeśli błąd -> odnawiamy listy
            PopulateDropdowns(dto.IdDruzyny, dto.IdKategorii, dto.IdZawodnika);
            return View(dto);
        }

        // GET: Robots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var robots = await _context.Roboty
                .Include(r => r.Team) // Warto widzieć nazwę drużyny przy usuwaniu
                .FirstOrDefaultAsync(m => m.IdRobota == id);

            if (robots == null) return NotFound();

            return View(robots);
        }

        // POST: Robots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var robots = await _context.Roboty.FindAsync(id);
            if (robots != null)
            {
                _context.Roboty.Remove(robots);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RobotsExists(int id)
        {
            return _context.Roboty.Any(e => e.IdRobota == id);
        }

        // --- TO JEST TA BRAKUJĄCA METODA ---
        private void PopulateDropdowns(int? selectedTeam = null, int? selectedCategory = null, int? selectedUser = null)
        {
            // 1. Kategorie
            ViewBag.Kategorie = new SelectList(_context.Kategorie, "IdKategorii", "NazwaKategorii", selectedCategory);

            // 2. Drużyny
            ViewBag.Druzyny = new SelectList(_context.Druzyny, "IdDruzyny", "NazwaDruzyny", selectedTeam);

            // 3. Zawodnicy (Operatorzy)
            // Tworzymy listę anonimową z polami Id i PelnaNazwa
            var users = _context.Zawodnicy.Select(u => new
            {
                Id = u.IdZawodnika,
                PelnaNazwa = u.Imie + " " + u.Nazwisko
            }).ToList();

            ViewBag.Zawodnicy = new SelectList(users, "Id", "PelnaNazwa", selectedUser);
        }
    }
}