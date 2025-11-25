using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Projekcik.Entities;
using Projekcik.Infrastructure.Persistance;
using Projekcik.application.Users; // Upewnij się, że ten namespace jest dodany dla DTO

namespace Projekcik.Controllers
{
    public class UsersController : Controller
    {
        private readonly AplicationDbContext _context;
        private readonly IMapper _mapper;

        public UsersController(AplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Zawodnicy.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Zawodnicy
                .FirstOrDefaultAsync(m => m.IdZawodnika == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Projekcik.Entities.Users users)
        {
            if (ModelState.IsValid)
            {
                users.Haslo = BCrypt.Net.BCrypt.HashPassword(users.Haslo);

                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(users);
        }

        // GET: Users/Edit/5
        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            // 1. Pobieramy z bazy
            var userEntity = await _context.Zawodnicy.FindAsync(id);
            if (userEntity == null) return NotFound();

            // 2. Mapujemy na EditDto (bezpieczne, bez hasła)
            var editDto = _mapper.Map<UserEditDto>(userEntity);

            // 3. Wysyłamy DTO do widoku
            return View(editDto);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserEditDto editDto)
        {
            if (id != editDto.IdZawodnika) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // 1. Pobierz ORYGINALNEGO użytkownika z bazy
                    var userEntity = await _context.Zawodnicy.FindAsync(id);
                    if (userEntity == null) return NotFound();

                    // 2. Mapuj zmiany: DTO -> Encja
                    // AutoMapper przepisze dane, ale stare hasło w userEntity zostanie zachowane
                    _mapper.Map(editDto, userEntity);

                    // 3. Zapisz zmiany
                    _context.Update(userEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(editDto.IdZawodnika)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(editDto);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Zawodnicy
                .FirstOrDefaultAsync(m => m.IdZawodnika == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Zawodnicy.FindAsync(id);
            if (users != null)
            {
                _context.Zawodnicy.Remove(users);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            return _context.Zawodnicy.Any(e => e.IdZawodnika == id);
        }

        // --- SEKCJA ZMIANY HASŁA (Naprawiona) ---

        // GET: Wyświetl formularz zmiany hasła
        [HttpGet]
        public IActionResult ChangePassword(int id)
        {
            return View(new ChangePasswordDto { IdZawodnika = id });
        }

        // POST: Przetwórz zmianę hasła
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var user = await _context.Zawodnicy.FindAsync(dto.IdZawodnika);
            if (user == null) return NotFound();

            // 1. Sprawdź czy STARE hasło pasuje do tego w bazie
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(dto.StareHaslo, user.Haslo);

            if (!isPasswordCorrect)
            {
                // Używamy klucza "StareHaslo" zgodnego z nazwą właściwości w DTO
                ModelState.AddModelError("StareHaslo", "Błędne aktualne hasło.");
                return View(dto);
            }

            // 2. Jeśli stare pasuje, zahaszuj NOWE hasło i nadpisz w bazie
            user.Haslo = BCrypt.Net.BCrypt.HashPassword(dto.NoweHaslo);

            _context.Update(user);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Hasło zostało zmienione pomyślnie!";
            return RedirectToAction(nameof(Details), new { id = dto.IdZawodnika });
        }
    }
}