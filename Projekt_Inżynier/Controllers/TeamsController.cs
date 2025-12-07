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
                .Include(t => t.Zawodnicy) 
                .Include(t => t.Roboty)    
                .ToListAsync();

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
        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var team = await _context.Druzyny
                .Include(t => t.Zawodnicy)
                .FirstOrDefaultAsync(m => m.IdDruzyny == id);

            if (team == null) return NotFound();

            var dto = new Projekcik.application.Teams.TeamEditDto
            {
                IdDruzyny = team.IdDruzyny,
                NazwaDruzyny = team.NazwaDruzyny,
                WybraneIdZawodnikow = team.Zawodnicy.Select(u => u.IdZawodnika).ToList()
            };

            // Ładujemy dane do checkboxów
            await PopulateUsersListData(dto);

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
                var teamToUpdate = await _context.Druzyny
                    .Include(t => t.Zawodnicy)
                    .FirstOrDefaultAsync(t => t.IdDruzyny == id);

                if (teamToUpdate == null) return NotFound();

                teamToUpdate.NazwaDruzyny = dto.NazwaDruzyny;

                // --- AKTUALIZACJA ZAWODNIKÓW (WERSJA ZOPTYMALIZOWANA) ---
                // 1. Czyścimy obecnych
                teamToUpdate.Zawodnicy.Clear();

                // 2. Pobieramy wszystkich wybranych jednym szybkim zapytaniem SQL
                if (dto.WybraneIdZawodnikow != null && dto.WybraneIdZawodnikow.Any())
                {
                    var selectedUsers = await _context.Zawodnicy
                        .Where(u => dto.WybraneIdZawodnikow.Contains(u.IdZawodnika))
                        .ToListAsync();

                    // 3. Dodajemy ich do drużyny
                    foreach (var user in selectedUsers)
                    {
                        teamToUpdate.Zawodnicy.Add(user);
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Jeśli błąd walidacji - załaduj listę ponownie
            await PopulateUsersListData(dto);
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

        // --- METODA POMOCNICZA (Żeby nie powtarzać kodu) ---
        private async Task PopulateUsersListData(Projekcik.application.Teams.TeamEditDto dto)
        {
            var wszyscy = await _context.Zawodnicy
                .Select(u => new
                {
                    Id = u.IdZawodnika,
                    PelnaNazwa = u.Imie + " " + u.Nazwisko
                })
                .ToListAsync();

            // Używamy MultiSelectList, bo łatwo sprawdzi, co zaznaczyć
            ViewBag.Uzytkownicy = new MultiSelectList(wszyscy, "Id", "PelnaNazwa", dto.WybraneIdZawodnikow);
        }
    }

}
