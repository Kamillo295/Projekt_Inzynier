using System.ComponentModel.DataAnnotations;

namespace Projekcik.application.Users
{
    public class ChangePasswordDto
    {
        public int IdZawodnika { get; set; }

        [Display(Name = "Aktualne hasło")]
        public string StareHaslo { get; set; } = default!;

        [Display(Name = "Nowe hasało")]
        public string NoweHaslo { get; set; } = default!;

        [Display(Name = "Powtórz nowe hasło")]
        public string PowtorzHaslo { get; set; } = default!;
    }
}