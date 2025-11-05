using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekcik.application.Users
{
    public class UsersDto 
    { 
        public string Imie { get; set; } = default!;
        public string Nazwisko { get; set; } = default!;
        public string NumerTelefonu { get; set; } = default!;
        public string Haslo { get; set; } = default!;
        public string? Email { get; set; }
        public string? RozmiarKoszulki { get; set; }
        public int? Wiek { get; set; }
        public string? KodPocztowy { get; set; }

    }
}
