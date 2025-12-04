using System.ComponentModel.DataAnnotations;

namespace Projekcik.Entities
{
    public class Robots
    {
        [Key] public int IdRobota { get; set; } = default;
        public int IdKategorii { get; set; } = default;
        public string NazwaRobota { get; set; } = default!;
        public int IdDruzyny { get; set; }
        public Team Team { get; set; }
    
        public Categories Categories { get; set; }
    }
}
