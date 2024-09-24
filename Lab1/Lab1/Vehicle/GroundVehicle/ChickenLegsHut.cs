
namespace Lab1.Vehicles
{
    public class ChickenLegsHut : GroundVehicle
    {
        public override double Speed { get; set; } = 5;
        public override double MoveTime { get; set; } = 150;

        protected override double GetRestTime()
        {
            return 5 * Math.Sin(RestCount);
        }
    }
}
