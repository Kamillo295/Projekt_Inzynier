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

namespace Projekcik.Controllers
{
    public class UsersController : Controller
    {
        private readonly AplicationDbContext _context;
        private readonly IMapper _mapper;
    
        public UsersController(AplicationDbContext context, IMapper mapper)     //tutaj mapowanie dto
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Projekcik.Entities.Users users)
        {
            if (ModelState.IsValid)
            {
               // var users = _mapper.Map<Projekcik.Entities.Users>(usersDto);
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
            var editDto = _mapper.Map<Projekcik.application.Users.UserEditDto>(userEntity);

            // 3. Wysyłamy DTO do widoku
            return View(editDto);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Przyjmujemy UserEditDto zamiast Users
        public async Task<IActionResult> Edit(int id, Projekcik.application.Users.UserEditDto editDto)
        {
            if (id != editDto.IdZawodnika) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // 1. Pobierz ORYGINALNEGO użytkownika z bazy (to ważne!)
                    var userEntity = await _context.Zawodnicy.FindAsync(id);
                    if (userEntity == null) return NotFound();

                    // 2. Mapuj zmiany: DTO -> Encja
                    // AutoMapper przepisze tylko imię, nazwisko, telefon itd.
                    // Pole 'Haslo' w userEntity pozostanie nienaruszone (stare hasło zostaje)!
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

            // Jeśli błąd walidacji, wracamy do widoku z tym samym DTO
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
    }
}
