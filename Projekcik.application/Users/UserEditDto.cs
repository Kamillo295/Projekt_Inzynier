using System.ComponentModel.DataAnnotations;
using Projekcik.Entities;

namespace Projekcik.application.Users
{
    public class UserEditDto
    {
        [Display(Name = "Numer")]
        public int IdZawodnika { get; set; }

        [Display(Name = "Imię")]
        public string? Imie { get; set; } = default!;

        [Display(Name = "Nazwisko")]
        public string? Nazwisko { get; set; } = default!;

        [Display(Name = "Numer telefonu")]
        public string? NumerTelefonu { get; set; } = default!;

        [Display(Name = "Adres e-mail")]
        public string? Email { get; set; }

        [Display(Name = "Rozmiar koszulki")]
        public RozmiarKoszulkiTyp? RozmiarKoszulki { get; set; }

        [Display(Name = "Wiek zawodnika")]
        public int? Wiek { get; set; }

        [Display(Name = "Kod pocztowy")]
        public string? KodPocztowy { get; set; }
    }

    public enum RozmiarKoszulkiTyp
    {
        XS,
        S,
        M,
        L,
        XL,
        XXL
    }

}