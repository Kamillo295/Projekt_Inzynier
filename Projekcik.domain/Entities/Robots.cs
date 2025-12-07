using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Projekcik.Entities
{
    public class Robots
    {
        [Key] public int IdRobota { get; set; }
        public string NazwaRobota { get; set; } = default!;

        // --- KATEGORIA ---
        public int IdKategorii { get; set; }

        [ForeignKey("IdKategorii")]
        public Categories Categories { get; set; }

        public int IdDruzyny { get; set; }

        [ForeignKey("IdDruzyny")]
        public Team Team { get; set; }

        // --- ZAWODNIK ---
        public int IdZawodnika { get; set; }

        [ForeignKey("IdZawodnika")]
        public Users Zawodnik { get; set; }
    }
}