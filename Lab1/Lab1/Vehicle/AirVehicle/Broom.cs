
namespace Lab1.Vehicles
{
    public class Broom : AirVehicle
    {
        public override double Speed { get; set; } = 5;
        public override double CalculateAcceleration(double distance)
        {
            return distance / 50;
        }
    }
}
