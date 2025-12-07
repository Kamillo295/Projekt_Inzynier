using System.ComponentModel.DataAnnotations;

namespace Projekcik.application.Robots
{
    public class RobotCreateDto
    {
        [Display(Name = "Nazwa Robota")]
        [Required(ErrorMessage = "Musisz podać nazwę robota.")]
        public string NazwaRobota { get; set; } = default!;

        [Display(Name = "Kategoria")]
        [Range(1, int.MaxValue, ErrorMessage = "Wybierz kategorię.")]   //inaczej wywala błąd jak nic się nie wpisze
        public int IdKategorii { get; set; }

        [Display(Name = "Drużyna")]
        [Range(1, int.MaxValue, ErrorMessage = "Wybierz drużynę.")]
        public int IdDruzyny { get; set; }

        [Display(Name = "Operator (Zawodnik)")]
        [Range(1, int.MaxValue, ErrorMessage = "Musisz przypisać operatora do robota!")]
        public int IdZawodnika { get; set; }
    }
}