
namespace Lab1.Vehicles
{
    public class BabaYagaMortar : AirVehicle
    {
        public override double Speed { get; set; } = 7;

        public override double CalculateAcceleration(double distance)
        {
            return distance * 0.666 + 1;
        }
    }
}
