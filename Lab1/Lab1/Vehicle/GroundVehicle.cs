
namespace Lab1.Vehicle
{
    internal abstract class GroundVehicle : Vehicle
    {
        public abstract double MoveTime { set; get; }
        public abstract double RestTime { set; get; }
        public bool IsInRest { set; get; } = false;
        public double LastRestEndTime { set; get; } = 0;

        protected bool canMove(double time)
        {
            if (IsInRest && time > LastRestEndTime)
            {
                IsInRest = false;
            }
            else if (time - LastRestEndTime > MoveTime)
            {
                IsInRest = true;
                LastRestEndTime = time + RestTime;
            }
            return !IsInRest;
        }
    }

    internal class PumpkinCarriage : GroundVehicle
    {
        public override double Speed { get; set; } = 13;
        public override double MoveTime { get; set; } = 20;
        public override double RestTime { get; set; } = 5;

        public override void Move(double time)
        {
            if (canMove(time))
            {

            }
        }
    }

    internal class Centaur : GroundVehicle
    {
        public override double Speed { get; set; } = 3;
        public override double MoveTime { get; set; } = 30;

        public override double RestTime { get; set; } = 3;

        public override void Move(double time)
        {
            if (canMove(time))
            {

            }
        }
    }

    internal class ChickenLegsHut : GroundVehicle
    {
        public override double Speed { get; set; } = 5;
        public override double MoveTime { get; set; }= 15;
        public override double RestTime { get; set; } = 5;

        public override void Move(double time)
        {
            if (canMove(time))
            {

            }
        }
    }

    internal class MagicBoots : GroundVehicle
    {
        public override double Speed { get; set;  } = 4;
        public override double MoveTime { get; set; } = 40;
        public override double RestTime { get; set; } = 10;


        public override void Move(double time)
        {
            if (canMove(time))
            {

            }
        }
    }
}
