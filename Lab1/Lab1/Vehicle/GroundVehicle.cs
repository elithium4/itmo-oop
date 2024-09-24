
namespace Lab1.Vehicles
{
    public abstract class GroundVehicle : Vehicle
    {
        public abstract double MoveTime { set; get; }
        public int RestCount { set; get; } = 0;
        public bool IsInRest { set; get; } = false;
        public double LastRestEndTime { set; get; } = 0;

        protected abstract double GetRestTime();

        protected bool canMove(double time)
        {
            if (IsInRest && time > LastRestEndTime)
            {
                IsInRest = false;
            }
            else if (time - LastRestEndTime > MoveTime)
            {
                IsInRest = true;
                LastRestEndTime = time + GetRestTime();
                RestCount++;
            }
            return !IsInRest;
        }

        public override void Move(double time)
        {
            if (canMove(time))
            {
                DistanceFromStart += Speed;
            }
        }
    }

    public class PumpkinCarriage : GroundVehicle
    {
        public override double Speed { get; set; } = 13;
        public override double MoveTime { get; set; } = 20;

        protected override double GetRestTime()
        {
            return 5;
        }
    }

    public class Centaur : GroundVehicle
    {
        public override double Speed { get; set; } = 7;
        public override double MoveTime { get; set; } = 300;

        protected override double GetRestTime()
        {
            return RestCount > 0 ? 3 / RestCount : 3;
        }
    }

    public class ChickenLegsHut : GroundVehicle
    {
        public override double Speed { get; set; } = 5;
        public override double MoveTime { get; set; } = 150;

        protected override double GetRestTime()
        {
            return 5 * Math.Sin(RestCount);
        }
    }

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
