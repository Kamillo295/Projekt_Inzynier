using System.ComponentModel.DataAnnotations;

namespace Projekcik.application.Users
{
    public class UsersDetailsDto
    {
        public int IdZawodnika { get; set; }
        public string Imie { get; set; } = default!;
        public string Nazwisko { get; set; } = default!;
        public string NumerTelefonu { get; set; } = default!;
        public string Email { get; set; } = default!;
        public RozmiarKoszulkiTyp RozmiarKoszulki { get; set; }
        public int Wiek { get; set; }
        public string? KodPocztowy { get; set; }
        public List<string> Roboty { get; set; } = new List<string>();
        public string Druzyna { get; set; } = default!;
    }
}