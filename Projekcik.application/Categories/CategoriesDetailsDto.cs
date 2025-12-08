using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekcik.application.Categories
{
    public class CategoriesDetailsDto
    {
        public int IdKategorii {  get; set; }
        public string NazwaKategorii { get; set; }

        public List<string> Roboty {  get; set; } = new List<string>();
    }
}
