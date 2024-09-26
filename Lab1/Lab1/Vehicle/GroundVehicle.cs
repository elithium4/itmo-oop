
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

}
