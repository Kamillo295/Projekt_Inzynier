using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // <--- Dodaj ten using

namespace Projekcik.Entities
{
    public class Robots
    {
        [Key] public int IdRobota { get; set; }
        public string NazwaRobota { get; set; } = default!;

        // --- KATEGORIA ---
        public int IdKategorii { get; set; }

        [ForeignKey("IdKategorii")] // Dobra praktyka: dodaj też tutaj
        public Categories Categories { get; set; }

        // --- DRUŻYNA (TU JEST PROBLEM) ---
        public int IdDruzyny { get; set; }

        // DODAJ TĘ LINIJKĘ PONIŻEJ:
        [ForeignKey("IdDruzyny")]
        public Team Team { get; set; }

        // --- ZAWODNIK ---
        public int IdZawodnika { get; set; }

        [ForeignKey("IdZawodnika")]
        public Users Zawodnik { get; set; }
    }
}