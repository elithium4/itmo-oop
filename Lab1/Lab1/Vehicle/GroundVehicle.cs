
namespace Lab1.Vehicle
{
    internal abstract class GroundVehicle : Vehicle
    {
        public abstract double MoveTime { set; get; }
        public int RestCount {set; get; } = 0;
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

        public override void Move(double time) {
            if (canMove(time))
            {
                DistanceFromStart += Speed;
            }
        }
    }

    internal class PumpkinCarriage : GroundVehicle
    {
        public override double Speed { get; set; } = 13;
        public override double MoveTime { get; set; } = 20;

        protected override double GetRestTime()
        {
            return 5;
        }
    }

    internal class Centaur : GroundVehicle
    {
        public override double Speed { get; set; } = 3;
        public override double MoveTime { get; set; } = 30;

        protected override double GetRestTime()
        {
            return 3 / RestCount;
        }
    }

    internal class ChickenLegsHut : GroundVehicle
    {
        public override double Speed { get; set; } = 5;
        public override double MoveTime { get; set; }= 15;

        protected override double GetRestTime()
        {
            return 5 * RestCount * RestCount;
        }
    }

    internal class MagicBoots : GroundVehicle
    {
        public override double Speed { get; set;  } = 4;
        public override double MoveTime { get; set; } = 40;

        protected override double GetRestTime()
        {
            return RestCount *  Math.Sin(RestCount);
        }
    }
}
