using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekcik.application.Categories
{
    public class CategoryDto
    {
        public int IdKategorii { get; set; }
        [Display(Name = "Nazwa Kategorii")]
        public string NazwaKategorii { get; set; }
        public int LiczbaRobotow { get; set; }

        // Opcjonalnie: Lista nazw robotów, jeśli chcesz je wyświetlić po przecinku
        public List<string> NazwyRobotow { get; set; } = new List<string>();
    }
}