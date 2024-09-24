
namespace Lab1.Vehicles
{
    public abstract class AirVehicle : Vehicle
    {
        public abstract double CalculateAcceleration(double distance);

        public override void Move(double time)
        {
            DistanceFromStart += Speed + CalculateAcceleration(DistanceFromStart);
        }
    }

    public class Broom : AirVehicle
    {
        public override double Speed { get; set; } = 5;
        public override double CalculateAcceleration(double distance)
        {
            return distance / 50;
        }
    }

    public class BabaYagaMortar : AirVehicle
    {
        public override double Speed { get; set; } = 7;

        public override double CalculateAcceleration(double distance)
        {
            return distance * 0.666 + 1;
        }
    }

    public class FlyingShip : AirVehicle
    {
        public override double Speed { get; set; } = 41;

        public override double CalculateAcceleration(double distance)
        {
            return 1;
        }
    }

    public class MagicCarpet : AirVehicle
    {
        public override double Speed { get; set; } = 8;

        public override double CalculateAcceleration(double distance)
        {
            return Math.Sqrt(distance) + Math.Sqrt(Math.Sqrt(distance));
        }
    }
}
