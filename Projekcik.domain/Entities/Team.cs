using System.ComponentModel.DataAnnotations;

namespace Projekcik.Entities
{
    public class Team
    {
        [Key] public int IdDruzyny { get; set; } = default;
        //public int IdZawodnika { get; set; } = default;
        public int IdRobota {  get; set; } = default;
        public string NazwaDruzyny { get; set; } = default;

        public ICollection<Users> Zawodnicy { get; set; } = new List<Users>();
        public ICollection<Robots> Roboty { get; set; } = new List<Robots>();

    }
}
