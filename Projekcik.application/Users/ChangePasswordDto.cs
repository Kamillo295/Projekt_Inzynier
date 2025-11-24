using System.ComponentModel.DataAnnotations;

namespace Projekcik.application.Users
{
    public class ChangePasswordDto
    {
        public int IdZawodnika { get; set; }

        [Required(ErrorMessage = "Podaj aktualne hasło")]
        public string StareHaslo { get; set; } = default!;

        [Required(ErrorMessage = "Podaj nowe hasło")]
        [MinLength(8, ErrorMessage = "Hasło musi mieć min. 8 znaków")]
        public string NoweHaslo { get; set; } = default!;

        [Compare("NoweHaslo", ErrorMessage = "Hasła nie są identyczne")]
        public string PowtorzHaslo { get; set; } = default!;
    }
}