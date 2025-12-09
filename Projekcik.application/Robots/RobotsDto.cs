using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Projekcik.application.Robots
{
    public class RobotsDto
    {
        public int IdRobota { get; set; }
        [Display(Name = "Nazwa Robota")]
        public string NazwaRobota { get; set; }

        // Pola do wyświetlania nazw (zamiast samych ID)
        [Display(Name = "Kategoria")]
        public string NazwaKategorii { get; set; }
        [Display(Name = "Drużyna")]
        public string NazwaDruzyny { get; set; }

        // Imię i nazwisko operatora
        [Display(Name = "Operator")]
        public string? NazwaOperatora { get; set; }
    }
}