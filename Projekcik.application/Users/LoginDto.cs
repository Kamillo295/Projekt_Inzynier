using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekcik.application.Users
{
    public class LoginDto
    {
        [Display(Name = "Email")]
        public string? Email { get; set; } = default!;

        [Display(Name = "Hasło")]
        public string? Haslo { get; set; } = default!;
    }
}
