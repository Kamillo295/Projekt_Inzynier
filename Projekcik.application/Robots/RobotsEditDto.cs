namespace Projekcik.application.Robots
{
    // Dziedziczymy po CreateDto, więc mamy już pola: Nazwa, IdKategorii, IdDruzyny, IdZawodnika
    public class RobotEditDto : RobotCreateDto
    {
        public int IdRobota { get; set; }
    }
}