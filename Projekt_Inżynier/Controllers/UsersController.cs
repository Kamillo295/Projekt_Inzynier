using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekcik.application.Users;
using Projekcik.Entities;
using Projekcik.Infrastructure.Persistance;
using System.Security.Claims;
using System.Text.Json;

namespace Projekcik.Controllers
{
    public class UsersController : Controller
    {
        private readonly AplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IEmailService _emailService;

        public UsersController(AplicationDbContext context, IMapper mapper, IConfiguration config, IEmailService emailService)
        {
            _dbContext = context;
            _mapper = mapper;
            _config = config;
            _emailService = emailService;
        }

        // --- LISTA UŻYTKOWNIKÓW ---
        public async Task<IActionResult> Index()
        {
            var usersEntities = await _dbContext.Zawodnicy.ToListAsync();
            // Mapowanie: Encja (Baza) -> DTO (Widok)
            var usersDtos = _mapper.Map<List<UsersDto>>(usersEntities);
            return View(usersDtos);
        }

        // --- SZCZEGÓŁY ---
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var user = await _dbContext.Zawodnicy
                .Include(t => t.Druzyny)  // Pobierz listę zawodników
               // .Include(t => t.Robot)     // Pobierz listę robotów
                .FirstOrDefaultAsync(m => m.IdZawodnika == id);


            return user == null ? NotFound() : View(user);
        }

        // --- LOGOWANIE ---
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            // 1. Walidacja wstępna (czy pola nie są puste)
            if (!ModelState.IsValid) return View(loginDto);

            // 2. Pobranie użytkownika
            var user = await _dbContext.Zawodnicy.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Użytkownik nie istnieje");
                return View(loginDto);
            }

            // 3. Weryfikacja hasła (Hash vs Jawne)
            if (!BCrypt.Net.BCrypt.Verify(loginDto.Haslo, user.Haslo))
            {
                ModelState.AddModelError("", "Niepoprawne hasło");
                return View(loginDto);
            }

            // 4. Budowanie ciasteczka (zalogowanie)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.IdZawodnika.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.Imie ?? "Użytkownik"),
                new Claim(ClaimTypes.Surname, user.Nazwisko ?? "")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        // --- REJESTRACJA (CREATE) ---
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsersDto usersDto)
        {
            // 1. Sprawdzenie reCAPTCHA
            if (!await VerifyRecaptcha(Request.Form["g-recaptcha-response"]))
            {
                ModelState.AddModelError("", "reCAPTCHA niezweryfikowana.");
            }

            // 2. Sprawdzenie unikalności maila
            if (await _dbContext.Zawodnicy.AnyAsync(u => u.Email == usersDto.Email))
            {
                ModelState.AddModelError("Email", "Taki adres email jest już zajęty.");
            }

            // 3. Jeśli są błędy -> powrót do formularza
            if (!ModelState.IsValid) return View(usersDto);

            // 4. Mapowanie i hashowanie hasła
            var userEntity = _mapper.Map<Users>(usersDto);
            userEntity.Haslo = BCrypt.Net.BCrypt.HashPassword(userEntity.Haslo);
            userEntity.EmailConfirmationToken = Guid.NewGuid().ToString();

            // 5. Zapis do bazy
            _dbContext.Add(userEntity);
            await _dbContext.SaveChangesAsync();

            // 6. Wysłanie maila
            var confirmLink = Url.Action("ConfirmEmail", "Users",
                new { id = userEntity.IdZawodnika, token = userEntity.EmailConfirmationToken }, Request.Scheme);

            await _emailService.SendEmailAsync(userEntity.Email, "Potwierdź konto",
                $"<a href='{confirmLink}'>Kliknij, aby potwierdzić</a>");

            return RedirectToAction(nameof(Index));
        }

        // --- EDYCJA ---
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var userEntity = await _dbContext.Zawodnicy.FindAsync(id);
            if (userEntity == null) return NotFound();

            // Mapujemy na bezpieczne DTO (bez hasła)
            var editDto = _mapper.Map<UserEditDto>(userEntity);
            return View(editDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, UserEditDto editDto)
        {
            if (id != editDto.IdZawodnika) return NotFound();

            if (!ModelState.IsValid) return View(editDto);

            var userEntity = await _dbContext.Zawodnicy.FindAsync(id);
            if (userEntity == null) return NotFound();

            // AutoMapper aktualizuje tylko dane personalne (hasło zostaje stare)
            _mapper.Map(editDto, userEntity);

            _dbContext.Update(userEntity);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // --- ZMIANA HASŁA ---
        [Authorize]
        public IActionResult ChangePassword(int id) => View(new ChangePasswordDto { IdZawodnika = id });

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var user = await _dbContext.Zawodnicy.FindAsync(dto.IdZawodnika);
            if (user == null) return NotFound();

            // Weryfikacja starego hasła
            if (!BCrypt.Net.BCrypt.Verify(dto.StareHaslo, user.Haslo))
            {
                ModelState.AddModelError("StareHaslo", "Błędne aktualne hasło.");
                return View(dto);
            }

            // Zapis nowego hasła
            user.Haslo = BCrypt.Net.BCrypt.HashPassword(dto.NoweHaslo);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = dto.IdZawodnika });
        }

        // --- USUWANIE ---
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var user = await _dbContext.Zawodnicy.FirstOrDefaultAsync(m => m.IdZawodnika == id);
            return user == null ? NotFound() : View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _dbContext.Zawodnicy.FindAsync(id);
            if (user != null)
            {
                _dbContext.Zawodnicy.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // --- POTWIERDZENIE MAILA ---
        public async Task<IActionResult> ConfirmEmail(int id, string token)
        {
            var user = await _dbContext.Zawodnicy.FindAsync(id);
            if (user == null || user.EmailConfirmationToken != token) return BadRequest("Błąd weryfikacji.");

            user.EmailConfirmed = true;
            user.EmailConfirmationToken = null;
            await _dbContext.SaveChangesAsync();

            return View("EmailConfirmed");
        }

        // --- METODY POMOCNICZE (PRIVATE) ---

        // Wydzielona logika ReCaptcha dla czytelności Create()
        private async Task<bool> VerifyRecaptcha(string responseToken)
        {
            if (string.IsNullOrEmpty(responseToken)) return false;

            var secretKey = _config["Recaptcha:SecretKey"];
            using var http = new HttpClient();
            var result = await http.PostAsync("https://www.google.com/recaptcha/api/siteverify",
                new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "secret", secretKey },
                    { "response", responseToken }
                }));

            if (!result.IsSuccessStatusCode) return false;

            var json = await result.Content.ReadAsStringAsync();
            var captchaResult = JsonSerializer.Deserialize<RecaptchaVerifyResponse>(json);

            return captchaResult?.success ?? false;
        }
    }
}