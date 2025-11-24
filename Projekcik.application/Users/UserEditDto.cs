using System.ComponentModel.DataAnnotations;
using Projekcik.Entities;

namespace Projekcik.application.Users
{
    public class UserEditDto
    {
        [Key]
        public int IdZawodnika { get; set; }

        [Required(ErrorMessage = "Imię jest wymagane")]
        [StringLength(50, ErrorMessage = "Imię nie może być dłuższe niż 50 znaków")]
        public string Imie { get; set; } = default!;

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        [StringLength(50, ErrorMessage = "Nazwisko nie może być dłuższe niż 50 znaków")]
        public string Nazwisko { get; set; } = default!;

        [Required(ErrorMessage = "Numer telefonu jest wymagany")]
        [Phone(ErrorMessage = "Niepoprawny format numeru telefonu")]
        public string NumerTelefonu { get; set; } = default!;

        [EmailAddress(ErrorMessage = "Niepoprawny format adresu email")]
        public string? Email { get; set; }

        [Display(Name = "Rozmiar Koszulki")]
        [EnumDataType(typeof(RozmiarKoszulkiTyp), ErrorMessage = "Niewłaściwy rozmiar")]
        public string? RozmiarKoszulki { get; set; }

        [Range(1, 120, ErrorMessage = "Wiek musi mieścić się w przedziale 1-120")]
        public int? Wiek { get; set; }

        public string? KodPocztowy { get; set; }

        public ICollection<Team> Druzyny { get; set; } = new List<Team>();
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