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
        public int LiczbaZawodnikow { get; set; }
        public List<string> NazwyRobotow { get; set; } = new List<string>();
    }
}
