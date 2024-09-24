
namespace Lab1.Vehicles
{
    public class Centaur : GroundVehicle
    {
        public override double Speed { get; set; } = 7;
        public override double MoveTime { get; set; } = 300;

        protected override double GetRestTime()
        {
            return RestCount > 0 ? 3 / RestCount : 3;
        }
    }
}

