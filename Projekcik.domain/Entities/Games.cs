using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekcik.Entities
{
    public class Games
    {
        [Key]
        public int ID { get; set; }

        public int Robot1ID { get; set; }
        public int Robot2ID { get; set; }

        [ForeignKey(nameof(Robot1ID))]
        public Robots Robot1 { get; set; }

        [ForeignKey(nameof(Robot2ID))]
        public Robots Robot2 { get; set; }

        public int Zwyciezca { get; set; }
        public int StopienDrabinki { get; set; }
    }
}