
namespace Lab1.Vehicles
{
    public class MagicBoots : GroundVehicle
    {
        public override double Speed { get; set; } = 15;
        public override double MoveTime { get; set; } = 40;

        protected override double GetRestTime()
        {
            return 1.1 * RestCount;
        }
    }
}
