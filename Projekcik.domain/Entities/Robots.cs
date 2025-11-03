using System.ComponentModel.DataAnnotations;

namespace Projekcik.Entities
{
    public class Robots
    {
        [Key] public int IdRobota { get; set; } = default;
        public int IdKategorii { get; set; } = default;
        public string NazwaRobota { get; set; } = default;
    }
}
