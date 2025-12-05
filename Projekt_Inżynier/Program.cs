using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Projekcik.Application.Mappings;
using Projekcik.Entities;
using Projekcik.Infrastructure.Persistance;
using Projekcik.infrastucture.Extenctions;

var builder = WebApplication.CreateBuilder(args);

// ==============================================================================
// KONFIGURACJA SERWISÓW (Kontener DI)
// ==============================================================================

// 1. Podstawowe serwisy aplikacji i infrastruktura (Baza danych)
builder.Services.AddControllersWithViews();
builder.Services.AddInfrastructure(builder.Configuration);

// 2. Narzêdzia zewnêtrzne (AutoMapper, FluentValidation)
builder.Services.AddAutoMapper(typeof(Projekcik.application.Users.UserEditDto).Assembly);

builder.Services.AddValidatorsFromAssemblyContaining<Projekcik.application.Users.UsersDtoValidator>()
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

// 3. Obs³uga Email
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();

// 4. Polityka Cookies (RODO / Zgody)
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true; // Wymagaj zgody
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
});

// 5. Autentykacja (Logowanie ciasteczkowe)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Users/Login";    // Œcie¿ka logowania
        options.LogoutPath = "/Users/Logout";  // Œcie¿ka wylogowania
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20); // Wa¿noœæ sesji
        options.SlidingExpiration = true;      // Odnawianie sesji przy aktywnoœci
    });

builder.Services.AddAuthorization();


// ==============================================================================
// KONFIGURACJA POTOKU ¯¥DAÑ (MIDDLEWARE PIPELINE)
// ==============================================================================
var app = builder.Build();

// 1. Obs³uga b³êdów w produkcji
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// 2. Polityka Cookies (musi byæ przed u¿yciem sesji/auth jeœli wymusza zgody)
app.UseCookiePolicy();

// 3. Routing (dopasowanie œcie¿ki do kontrolera)
app.UseRouting();

// 4. Bezpieczeñstwo (WA¯NE: Musi byæ PO UseRouting, a PRZED MapControllerRoute)
app.UseAuthentication(); // Kto to jest? (odczyt ciasteczka)
app.UseAuthorization();  // Czy mo¿e tu wejœæ? (sprawdzenie ról/policy)

// 5. Mapowanie koñcówek
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();