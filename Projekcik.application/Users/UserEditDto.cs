using System.ComponentModel.DataAnnotations;

namespace Projekcik.application.Users
{
    public class UserEditDto
    {
        // Potrzebujemy ID, żeby wiedzieć kogo edytujemy (często przydatne w widoku jako hidden field)
        public int IdZawodnika { get; set; }

        [Required(ErrorMessage = "Imię jest wymagane")]
        public string Imie { get; set; } = default!;

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        public string Nazwisko { get; set; } = default!;

        [Required(ErrorMessage = "Numer telefonu jest wymagany")]
        [Phone(ErrorMessage = "Niepoprawny format numeru")]
        public string NumerTelefonu { get; set; } = default!;

        [EmailAddress(ErrorMessage = "Niepoprawny email")]
        public string? Email { get; set; }

        public string? RozmiarKoszulki { get; set; }
        public int? Wiek { get; set; }
        public string? KodPocztowy { get; set; }

        // BRAK pola Haslo - to kluczowa różnica!
    }
}