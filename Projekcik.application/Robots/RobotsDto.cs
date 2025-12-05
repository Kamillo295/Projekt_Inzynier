using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekcik.application.Robots
{
    public class RobotsDto
    {
        public int IdRobota { get; set; }
        public string NazwaRobota { get; set; }

        // Klucze obce (przydatne przy edycji, żeby ustawić "selected" w dropdownie)
        public int IdKategorii { get; set; }
        public int IdDruzyny { get; set; }

        // Pola "Spłaszczone" - tylko do wyświetlania (Read-Only)
        // AutoMapper wyciągnie tu nazwy z relacji
        public string? NazwaKategorii { get; set; }
        public string? NazwaDruzyny { get; set; }
    }
}