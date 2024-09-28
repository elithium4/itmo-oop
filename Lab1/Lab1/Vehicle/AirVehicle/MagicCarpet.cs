namespace Lab1.Vehicles
{
    public class MagicCarpet : AirVehicle
    {
        public override double Speed { get; set; } = 8;

        public override double CalculateAcceleration(double distance)
        {
            return Math.Sqrt(distance) + Math.Sqrt(Math.Sqrt(distance));
        }
    }
}
