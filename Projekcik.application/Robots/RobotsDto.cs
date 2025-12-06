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

        // Pola do wyświetlania nazw (zamiast samych ID)
        public string NazwaKategorii { get; set; }
        public string NazwaDruzyny { get; set; }

        // Imię i nazwisko operatora
        public string? NazwaOperatora { get; set; }
    }
}