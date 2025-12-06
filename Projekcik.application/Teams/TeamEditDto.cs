using System.ComponentModel.DataAnnotations;

namespace Projekcik.application.Teams
{
    public class TeamEditDto
    {
        public int IdDruzyny { get; set; }

        [Display(Name = "Nazwa Drużyny")]
        public string NazwaDruzyny { get; set; } = default!;

        // To pole przechowa ID użytkowników zaznaczonych na liście
        [Display(Name = "Wybierz członków drużyny")]
        public List<int> WybraneIdZawodnikow { get; set; } = new List<int>();
    }
}