using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekcik.application.Teams
{
    public class TeamDto
    {
        public int IdDruzyny { get; set; }
        public string NazwaDruzyny { get; set; }

        // --- Pola wyliczalne (do wyświetlania) ---

        // Zamiast całej listy obiektów Users, wyświetlamy tylko ilu ich jest
        public int LiczbaZawodnikow { get; set; }

        // Zamiast pełnych obiektów Robots, wyświetlamy listę ich nazw
        public List<string> NazwyRobotow { get; set; } = new List<string>();

        // Ewentualnie, jeśli potrzebujesz imion zawodników po przecinku:
        // public string ImionaZawodnikow { get; set; }
    }
}
