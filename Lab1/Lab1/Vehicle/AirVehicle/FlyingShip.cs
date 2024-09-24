namespace Lab1.Vehicles
{
    public class FlyingShip : AirVehicle
    {
        public override double Speed { get; set; } = 41;

        public override double CalculateAcceleration(double distance)
        {
            return 1;
        }
    }
}
