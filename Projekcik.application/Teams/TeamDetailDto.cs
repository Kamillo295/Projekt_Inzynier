namespace Projekcik.application.Teams
{
    public class TeamDetailsDto
    {
        public int IdDruzyny { get; set; }
        public string NazwaDruzyny { get; set; } = default!;

        // Lista imion i nazwisk zawodników
        public List<string> Zawodnicy { get; set; } = new List<string>();

        // Lista nazw robotów
        public List<string> Roboty { get; set; } = new List<string>();
    }
}