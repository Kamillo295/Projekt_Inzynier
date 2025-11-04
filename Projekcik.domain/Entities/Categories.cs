using System.ComponentModel.DataAnnotations;

namespace Projekcik.Entities
{
    public class Categories
    {
        [Key] public int IdKategorii { get; set; } = default;
        public string NazwaKategorii { get; set; } = default;

        public ICollection<Robots> Roboty { get; set; } = new List<Robots>();
    }
}
