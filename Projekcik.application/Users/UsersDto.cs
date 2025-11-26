using System.ComponentModel.DataAnnotations; // Pamiętaj o tym usingu!

namespace Projekcik.application.Users
{
    public class UsersDto
    {
        public int IdZawodnika { get; set; }

        [Display(Name = "Imię")] // To zmieni nagłówek w tabeli i etykietę w formularzu
        public string Imie { get; set; } = default!;

        [Display(Name = "Nazwisko")]
        public string Nazwisko { get; set; } = default!;

        [Display(Name = "Numer telefonu")]
        public string NumerTelefonu { get; set; } = default!;

        [Display(Name = "Hasło")]
        public string Haslo { get; set; } = default!;

        [Display(Name = "Adres e-mail")]
        public string? Email { get; set; }

        [Display(Name = "Rozmiar koszulki")]
        public string? RozmiarKoszulki { get; set; }

        [Display(Name = "Wiek zawodnika")]
        public int? Wiek { get; set; }

        [Display(Name = "Kod pocztowy")]
        public string? KodPocztowy { get; set; }
    }
}