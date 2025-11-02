using System.ComponentModel.DataAnnotations;

namespace Projekcik.Entities
{
    public class Users
    {
        [Key] public int IdZawodnika { get; set; } = default;
        public string Imie { get; set; } = default;
        public string Nazwisko { get; set; } = default;
        public string NumerTelefonu { get; set; } = default;
        public string Haslo { get; set; } = default;
        public string? Email { get; set; }
        public string? RozmiarKoszulki { get; set; }
        public int? Wiek { get; set; }
        public string? KodPocztowy { get; set; }
    }
}
