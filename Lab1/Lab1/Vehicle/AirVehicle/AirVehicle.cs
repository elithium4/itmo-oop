
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

}
