
namespace Lab1.Vehicle
{
    internal abstract class AirVehicle : Vehicle
    {
        public abstract double CalculateAcceleration(double distance);

        public override void Move(double time)
        {
            DistanceFromStart += Speed + CalculateAcceleration(DistanceFromStart);
        }
    }

    internal class Broom : AirVehicle
    {
        public override double Speed { get; set; } = 5;
        public override double CalculateAcceleration(double distance)
        {
            return distance / 5;
        }

        // public override void Move(double time)
        // {
        //     DistanceFromStart += Speed + CalculateAcceleration(DistanceFromStart);
        // }
    }

    internal class BabaYagaMortar : AirVehicle
    {
        public override double Speed { get; set; } = 7;

        public override double CalculateAcceleration(double distance)
        {
            return distance * 0.666 + 1;
        }

        // public override void Move(double time)
        // {
        //     throw new NotImplementedException();
        // }
    }

    internal class FlyingShip : AirVehicle
    {
        public override double Speed { get; set; } = 11;

        public override double CalculateAcceleration(double distance)
        {
            return 1;
        }

        // public override void Move(double time)
        // {
        //     throw new NotImplementedException();
        // }
    }

    internal class MagicCarpet : AirVehicle
    {
        public override double Speed { get; set; } = 8;

        public override double CalculateAcceleration(double distance)
        {
            return Math.Sqrt(distance) * 0.5 + Math.Sqrt(Math.Sqrt(distance));
        }

        // public override void Move(double time)
        // {
        //     throw new NotImplementedException();
        // }
    }
}
