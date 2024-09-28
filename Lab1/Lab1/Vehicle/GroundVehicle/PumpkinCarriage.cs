

namespace Lab1.Vehicles
{
    public class PumpkinCarriage : GroundVehicle
    {
        public override double Speed { get; set; } = 13;
        public override double MoveTime { get; set; } = 20;

        protected override double GetRestTime()
        {
            return 5;
        }
    }
}
