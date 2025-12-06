using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekcik.application.Teams;
using Projekcik.Entities;
using Projekcik.Infrastructure.Persistance;

namespace Projekcik.Controllers
{
    public class TeamsController : Controller
    {
        private readonly AplicationDbContext _context;
        private readonly IMapper _mapper;

        public TeamsController(AplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        // GET: Teams
        public async Task<IActionResult> Index()
        {
            var teams = await _context.Druzyny
                .Include(t => t.Zawodnicy) // Załaduj listę zawodników
                .Include(t => t.Roboty)    // Załaduj listę robotów
                .ToListAsync();

            // AutoMapper przeliczy .Count i zamieni roboty na nazwy
            var teamDtos = _mapper.Map<List<TeamDto>>(teams);

            return View(teamDtos);
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var team = await _context.Druzyny
                .Include(t => t.Zawodnicy)  // Pobierz listę zawodników
                .Include(t => t.Roboty)     // Pobierz listę robotów
                .FirstOrDefaultAsync(m => m.IdDruzyny == id);

            if (team == null) return NotFound();

            // Mapowanie na DTO (ręczne dla czytelności, można też użyć AutoMappera)
            var dto = new Projekcik.application.Teams.TeamDetailsDto
            {
                IdDruzyny = team.IdDruzyny,
                NazwaDruzyny = team.NazwaDruzyny,

                // Zamieniamy listę obiektów Users na listę napisów "Imię Nazwisko"
                Zawodnicy = team.Zawodnicy
                    .Select(u => $"{u.Imie} {u.Nazwisko}")
                    .ToList(),

                // Zamieniamy listę obiektów Robots na listę nazw robotów
                Roboty = team.Roboty
                    .Select(r => r.NazwaRobota)
                    .ToList()
            };

            return View(dto);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDruzyny,IdRobota,NazwaDruzyny")] Team team)
        {
            if (ModelState.IsValid)
            {
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }
        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            // 1. Pobieramy drużynę razem z jej OBECNYMI zawodnikami
            var team = await _context.Druzyny
                .Include(t => t.Zawodnicy) // <--- KLUCZOWE: Musimy wiedzieć, kto już tam jest
                .FirstOrDefaultAsync(m => m.IdDruzyny == id);

            if (team == null) return NotFound();

            // 2. Tworzymy DTO i zaznaczamy obecnych członków
            var dto = new Projekcik.application.Teams.TeamEditDto
            {
                IdDruzyny = team.IdDruzyny,
                NazwaDruzyny = team.NazwaDruzyny,
                // Pobieramy same ID obecnych zawodników
                WybraneIdZawodnikow = team.Zawodnicy.Select(u => u.IdZawodnika).ToList()
            };

            // 3. TWORZYMY LISTĘ WSZYSTKICH UŻYTKOWNIKÓW Z BAZY
            // To tutaj realizujemy Twój wymóg: "lista już utworzonych użytkowników"
            var wszyscyUzytkownicy = _context.Zawodnicy
                .Select(u => new { Id = u.IdZawodnika, PelnaNazwa = u.Imie + " " + u.Nazwisko })
                .ToList();

            // MultiSelectList(Źródło, "Wartość", "Tekst", "CoMaByćZaznaczone")
            ViewBag.Uzytkownicy = new MultiSelectList(wszyscyUzytkownicy, "Id", "PelnaNazwa", dto.WybraneIdZawodnikow);

            return View(dto);
        }

        // POST: Teams/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Projekcik.application.Teams.TeamEditDto dto)
        {
            if (id != dto.IdDruzyny) return NotFound();

            if (ModelState.IsValid)
            {
                // 1. Pobieramy edytowaną drużynę z bazy (z relacjami!)
                var teamToUpdate = await _context.Druzyny
                    .Include(t => t.Zawodnicy)
                    .FirstOrDefaultAsync(t => t.IdDruzyny == id);

                if (teamToUpdate == null) return NotFound();

                // 2. Aktualizujemy zwykłe pola
                teamToUpdate.NazwaDruzyny = dto.NazwaDruzyny;

                // 3. AKTUALIZACJA CZŁONKÓW DRUŻYNY
                // Najprostsza metoda: wyczyść starych i dodaj zaznaczonych na nowo
                teamToUpdate.Zawodnicy.Clear();

                if (dto.WybraneIdZawodnikow != null)
                {
                    foreach (var userId in dto.WybraneIdZawodnikow)
                    {
                        // Szukamy użytkownika w bazie i dodajemy do drużyny
                        var user = await _context.Zawodnicy.FindAsync(userId);
                        if (user != null)
                        {
                            teamToUpdate.Zawodnicy.Add(user);
                        }
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Jeśli walidacja nie przejdzie, musimy odnowić listę (ViewBag znika)
            var wszyscy = _context.Zawodnicy
                .Select(u => new { Id = u.IdZawodnika, PelnaNazwa = u.Imie + " " + u.Nazwisko })
                .ToList();
            ViewBag.Uzytkownicy = new MultiSelectList(wszyscy, "Id", "PelnaNazwa", dto.WybraneIdZawodnikow);

            return View(dto);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Druzyny
                .FirstOrDefaultAsync(m => m.IdDruzyny == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _context.Druzyny.FindAsync(id);
            if (team != null)
            {
                _context.Druzyny.Remove(team);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
            return _context.Druzyny.Any(e => e.IdDruzyny == id);
        }
    }
}
