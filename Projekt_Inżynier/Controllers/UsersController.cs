using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Projekcik.application.Users;
using Projekcik.Entities;
using Projekcik.Infrastructure.Persistance;

namespace Projekcik.Controllers
{
    public class UsersController : Controller
    {
        private readonly AplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public UsersController(AplicationDbContext context, IMapper mapper, IConfiguration config)     //tutaj mapowanie dto
        {
            _dbContext = context;
            _mapper = mapper;
            _config = config;
        }

        //public Task<Projekcik.Entities.Users?> GetByName(string name)
        //=> _dbContext.Users.FirstOrDefaultAsync(cw => cw.Name.ToLower() == name.ToLower());


        //public Task<Projekcik.Entities.Users?> GetByName(string name)
        //=> _dbContext.Zawodnicy.FirstOrDefaultAsync(cw => cw.Imie.ToLower() == name.ToLower());

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _dbContext.Zawodnicy.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _dbContext.Zawodnicy
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsersDto usersDto)
        {
            var recaptchaResponse = Request.Form["g-recaptcha-response"];
            var secretKey = _config["Recaptcha:SecretKey"];

            using var http = new HttpClient();
            var result = await http.PostAsync(
                "https://www.google.com/recaptcha/api/siteverify",
                new FormUrlEncodedContent(new Dictionary<string, string>
                {
            { "secret", secretKey },
            { "response", recaptchaResponse }
                })
            );

            var json = await result.Content.ReadAsStringAsync();
            var captchaResult = System.Text.Json.JsonSerializer.Deserialize<RecaptchaVerifyResponse>(json);

            if (captchaResult == null || !captchaResult.success)
            {
                ModelState.AddModelError("", "reCAPTCHA niezweryfikowana. Udowodnij nam, że jesteś człowiekiem 😉");
            }

            if (!ModelState.IsValid)
            {
                return View(usersDto);
            }

            var users = _mapper.Map<Users>(usersDto);
            users.Haslo = BCrypt.Net.BCrypt.HashPassword(users.Haslo);

            _dbContext.Add(userEntity);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));



        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            // 1. Pobieramy z bazy
            var userEntity = await _dbContext.Zawodnicy.FindAsync(id);
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
                    var userEntity = await _dbContext.Zawodnicy.FindAsync(id);
                    if (userEntity == null) return NotFound();

                    // 2. Mapuj zmiany: DTO -> Encja
                    // AutoMapper przepisze tylko imię, nazwisko, telefon itd.
                    // Pole 'Haslo' w userEntity pozostanie nienaruszone (stare hasło zostaje)!
                    _mapper.Map(editDto, userEntity);

                    // 3. Zapisz zmiany
                    _dbContext.Update(userEntity);
                    await _dbContext.SaveChangesAsync();
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

            var users = await _dbContext.Zawodnicy
                .FirstOrDefaultAsync(m => m.IdZawodnika == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Wyświetl formularz zmiany hasła
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

            var user = await _dbContext.Zawodnicy.FindAsync(dto.IdZawodnika);
            if (user == null) return NotFound();

            // 1. Sprawdź czy STARE hasło pasuje do tego w bazie
            // BCrypt.Verify(tekstJawny, hashZBazy)
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(dto.StareHaslo, user.Haslo);

            if (!isPasswordCorrect)
            {
                ModelState.AddModelError("StareHaslo", "Błędne aktualne hasło.");
                return View(dto);
            }

            // 2. Jeśli stare pasuje, zahaszuj NOWE hasło i nadpisz w bazie
            user.Haslo = BCrypt.Net.BCrypt.HashPassword(dto.NoweHaslo);

            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();

            // Sukces! Przekieruj np. do listy lub szczegółów
            TempData["Message"] = "Hasło zostało zmienione pomyślnie!";
            return RedirectToAction(nameof(Details), new { id = dto.IdZawodnika });
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _dbContext.Zawodnicy.FindAsync(id);
            if (users != null)
            {
                _dbContext.Zawodnicy.Remove(users);
            }

            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            return _dbContext.Zawodnicy.Any(e => e.IdZawodnika == id);
        }
    }
}
