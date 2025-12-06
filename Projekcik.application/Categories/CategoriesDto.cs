using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekcik.application.Categories
{
    public class CategoryDto
    {
        public int IdKategorii { get; set; }
        public string NazwaKategorii { get; set; }

        // --- Pola wyliczalne (Read-Only) ---

        // Dzięki temu w tabeli kategorii zobaczysz np. "Sumo (5 robotów)"
        public int LiczbaRobotow { get; set; }

        // Opcjonalnie: Lista nazw robotów, jeśli chcesz je wyświetlić po przecinku
        public List<string> NazwyRobotow { get; set; } = new List<string>();
    }
}